﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using EFTHelper.Enums;

namespace EFTHelper.Services;

public class ThemeService
{
    #region Fields

    private static readonly Random _random = new();
    private readonly Application _application;
    private readonly ISettingsService _settingsService;

    #endregion

    #region Constructors

    public ThemeService(Application application, ISettingsService settingsService)
    {
        _settingsService = settingsService;
        _application = application;
    }

    #endregion

    #region Properties

    public Theme Theme => _settingsService.GetTheme();

    public Scheme Scheme => _settingsService.GetScheme();

    #endregion

    #region Methods

    public IEnumerable<Scheme> GetSchemes() => GetEnumValues<Scheme>();

    public IEnumerable<Theme> GetThemes() => GetEnumValues<Theme>();

    public void Apply()
    {
        ControlzEx.Theming.ThemeManager.Current.ChangeTheme(_application, $"{_settingsService.GetTheme()}.{_settingsService.GetScheme()}");
    }

    public void Change(Theme theme, Scheme scheme)
    {
        _settingsService.SetScheme(scheme);
        _settingsService.SetTheme(theme);
        _settingsService.Save();
        ControlzEx.Theming.ThemeManager.Current.ChangeTheme(_application, $"{theme}.{scheme}");
    }

    public (Theme Theme, Scheme Scheme) Random()
    {
        var theme = RandomEnumValue<Theme>();
        var scheme = RandomEnumValue<Scheme>();
        Change(theme, scheme);

        return (theme, scheme);
    }

    private static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(_random.Next(v.Length));
    }

    private static IEnumerable<T> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToArray().OrderBy(t => t);
    }

    #endregion
}
