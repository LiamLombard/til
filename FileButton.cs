using Godot;
using System;

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
		GD.Print("Open file");
		FileDialog fd = GetNode<FileDialog>("/root/Window/CenterContainer/FileDialog");
		fd.Show();
	}

	private void PushPressed()
	{
		GD.Print("Push file");
	}

	public void OpenFile(String path)
	{
		Window w = GetNode<Window>("/root/Window");
		w.SaveFilePath(path);

		TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
		textPreview.SetTextFromFile(path);

		HBoxContainer bottomButtons = GetNode<HBoxContainer>("/root/Window/VB/BottomHB");
		bottomButtons.Show();
	}

	private void OnCloseFile()
	{
		Window w = GetNode<Window>("/root/Window");
		w.SaveFilePath("");

		TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
		textPreview.ClearText();

		HBoxContainer bottomButtons = GetNode<HBoxContainer>("/root/Window/VB/BottomHB");
		bottomButtons.Hide();
	}
}
