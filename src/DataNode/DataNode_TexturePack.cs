﻿using PsychoPortal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PsychonautsTools;

public class DataNode_TexturePack : DataNode
{
    public DataNode_TexturePack(TexturePack texturePack, string fileName)
    {
        TexturePack = texturePack;
        FileName = fileName;
    }

    public TexturePack TexturePack { get; }
    public string FileName { get; }

    public override string TypeDisplayName => "Texture Pack";
    public override string DisplayName => FileName;
    public override bool HasChildren => TexturePack.Textures.AnyAndNotNull() ||
                                        TexturePack.LocalizedTextures.AnyAndNotNull();
    public override IBinarySerializable SerializableObject => TexturePack;

    public override IEnumerable<DataNode> CreateChildren(FileContext fileContext)
    {
        var textures = TexturePack.Textures.Select(x => new { Texture = x, FileName = x.FileName.Value });

        if (TexturePack.LocalizedTextures != null)
            foreach (LocalizedTextures locTextures in TexturePack.LocalizedTextures)
                textures = textures.Concat(locTextures.Textures.Select(x => new
                {
                    Texture = x, 
                    FileName = x.FileName.Value.Insert(x.FileName.Value.LastIndexOf('.'), $"_{locTextures.Language.ToString().ToLower()}"),
                }));

        yield return DataNode_Folder.FromTypedFiles(
            files: textures, 
            getFilePathFunc: x => x.FileName, 
            createFileNodeFunc: (file, fileName) => new Lazy<DataNode>(() => new DataNode_GameTexture(file.Texture, fileName)));
    }
}