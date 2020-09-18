using Godot;
using System;
using System.IO;

using File = System.IO.File;

public class Window : Control
{
	public ConfigFile config = new ConfigFile();
	public string config_path = "user://config.cfg";

	public override void _Ready()
	{
		var error = this.config.Load(config_path);
		if(error == Godot.Error.Ok)
		{
			if(this.config.HasSectionKey("file", "path"))
			{
				OpenFile(this.GetFilePath());
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

	public void OpenFile(string path)
	{
		if(File.Exists(path))
		{
			SaveFilePath(path);

			TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
			textPreview.SetTextFromFile(path);

			HBoxContainer bottomButtons = GetNode<HBoxContainer>("/root/Window/VB/BottomHB");
			bottomButtons.Show();
		}
	}

	public void CloseFile()
	{
		SaveFilePath("");

		TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
		textPreview.ClearText();

		HBoxContainer bottomButtons = GetNode<HBoxContainer>("/root/Window/VB/BottomHB");
		bottomButtons.Hide();
	}
}
