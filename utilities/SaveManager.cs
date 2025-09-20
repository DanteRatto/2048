using Godot;
using System;
using System.Threading.Tasks;

public partial class SaveManager : Node
{
    private const string saveFile = "user://save.cfg";

    private bool loaded;

    public ConfigFile ConfigFile
    {
        get
        {
            if (!loaded) configFile.Load(saveFile);
            loaded = true;
            return configFile;
        }
    }

    private readonly ConfigFile configFile = new();

    public override void _Ready()
    {
        base._Ready();
        if (!loaded) ConfigFile.Load(saveFile);
        loaded = true;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        ConfigFile.Save(saveFile);
    }

    public override async void _Notification(int what)
    {
        try
        {
            if (what == NotificationApplicationFocusOut || what == NotificationWMWindowFocusOut || what == NotificationWMCloseRequest)
            {
                await Task.Yield(); // wait a frame so all save data is up to date
                ConfigFile.Save(saveFile);
                if (what == NotificationWMCloseRequest) GetTree().Quit();
            }
        }
        catch (Exception e)
        {
            GD.PrintErr(e);
        }
        finally
        {
            base._Notification(what);
        }
    }
}
