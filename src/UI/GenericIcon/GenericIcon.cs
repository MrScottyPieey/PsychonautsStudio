﻿using System.Windows.Media;
using MahApps.Metro.IconPacks;

namespace PsychonautsTools;

public struct GenericIcon
{
    public GenericIcon(PackIconMaterialKind iconKind, Brush iconColor)
    {
        IconKind = iconKind;
        IconColor = iconColor;
    }

    public PackIconMaterialKind IconKind { get; set; }
    public Brush IconColor { get; set; }
}