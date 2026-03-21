using Godot;
using System;
using Util;

public partial class MenuCursor : TextureRect, ISkinnable
{
    private static SettingsProfile settings;
    public static MenuCursor Instance;

    public override void _Ready()
    {
        Instance = this;

        settings = SettingsManager.Instance.Settings;

        settings.CursorScale.Updated += (value) => UpdateSize((float)value);
        settings.UseCursorInMenus.Updated += (value) => UpdateVisible((bool)value);
        SkinManager.Instance.Loaded += UpdateSkin;

        UpdateSize(settings.CursorScale.Value);
        UpdateVisible(settings.UseCursorInMenus.Value);
        UpdateSkin();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Modulate = settings.RainbowCursor.Value ? Rainbow.HueToColor() : new Color(1,1,1,1);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
		{
            Position = mouseMotion.Position - Size / 2;
        }
    }

	public void UpdateSize(float size)
	{
        Size = Vector2.One * 32 * size;
    }

    public void UpdateVisible(bool visible, bool updateNativeCursor = true)
    {
        Visible = visible;

        if (updateNativeCursor)
        {
            Input.MouseMode = visible ? Input.MouseModeEnum.Hidden : Input.MouseModeEnum.Visible;
        }
    }

	public void UpdateSkin(SkinProfile skin = null)
    {
        skin ??= SkinManager.Instance.Skin;

        Texture = skin.CursorImage;
    }
}
