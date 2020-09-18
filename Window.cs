using Godot;
using System;

public class Window : Control
{
	public ConfigFile config = new ConfigFile();
	public string config_path = "user://config.cfg";

	public override void _Ready()
	{
		var error = this.config.Load(config_path);
		if(error != Godot.Error.Ok)
		{
			// Directory dir = new Directory();
			// if (dir.DirExists(config_dir))
			// {
			// 	if(dir.MakeDir(config_dir) != Godot.Error.Ok)
			// 	{
			// 		GD.Print("Could not make directory");
			// 	}
			// }
			// else
			// {
			// 	GD.Print("Issue with file");
			// }
		}
		else
		{
			if(this.config.HasSectionKey("file", "path"))
			{
				FileButton fb = GetNode<FileButton>("VB/TopHB/FileButton");
				fb.OpenFile(this.GetFilePath());
			}
		}

	}

	public void SaveFilePath(string path)
	{
		this.config.SetValue("file", "path", path);
		this.config.Save(config_path);
	}

	public string GetFilePath()
	{
		return this.config.GetValue("file", "path").ToString();
	}
}
