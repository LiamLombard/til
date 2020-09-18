using Godot;
using System;
using System.IO;

public class EditButton : Button
{
	private TextEdit editor;
	private TextPreview preview;
	private Window window;
	private SaveButton saveButton;
	private AddButton addButton;

	public override void _Ready()
	{
		editor = GetNode<TextEdit>("/root/Window/VB/MainHB/Editor");
		preview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
		window = GetNode<Window>("/root/Window");
		saveButton = GetNode<SaveButton>("/root/Window/VB/BottomHB/SaveButton");
		addButton = GetNode<AddButton>("/root/Window/VB/BottomHB/AddButton");
	}

	private void Pressed()
	{
		string path = window.GetFilePath();
		if(!path.Empty())
		{
			string fileContents = System.IO.File.ReadAllText(path);
			editor.Text = fileContents;

			// Hide unneeded
			TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
			textPreview.Hide();

			VSeparator seperator = GetNode<VSeparator>("/root/Window/VB/MainHB/VSeparator");
			seperator.Hide();
		}
		else
		{
			AcceptDialog ad = GetNode<AcceptDialog>("/root/Window/Notifications/NoFile");
			ad.Show();
			GD.Print("Path is not set");
		}
		this.Hide();
		addButton.Hide();
		saveButton.Show();
	}
}



