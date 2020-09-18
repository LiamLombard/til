using Godot;
using System;
using System.Runtime.InteropServices;

using Environment = System.Environment;

public class FileButton : MenuButton
{
	public override void _Ready()
	{
		PopupMenu fileOptions = GetPopup();
		fileOptions.Connect("id_pressed", this, "IdPressed");
	}

	private void IdPressed(int id)
	{
		if(id == 0)
		{
			OpenPressed();
		}
		else if (id == 1)
		{
			PushPressed();
		}
		else if (id == 2)
		{
			OnCloseFile();
		}
	}

	private void OpenPressed()
	{
		FileDialog fd = GetNode<FileDialog>("/root/Window/CenterContainer/FileDialog");
		fd.ClearFilters();
		string homePath = Environment.GetEnvironmentVariable("HOME");
		fd.CurrentDir = homePath;
		fd.CurrentPath = $"{homePath}/";
		fd.Popup_();
	}

	private void PushPressed()
	{
		GD.Print("Push file");
	}

	public void OpenFile(String path)
	{
		Window w = GetNode<Window>("/root/Window");
		w.OpenFile(path);
	}

	private void OnCloseFile()
	{
		Window w = GetNode<Window>("/root/Window");
		w.CloseFile();
	}
}
