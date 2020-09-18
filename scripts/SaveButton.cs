using Godot;
using System;
using System.IO;

public class SaveButton : Button
{
	private TextEdit editor;
	private TextPreview preview;
	private Window window;
	private EditButton editButton;
	private AddButton addButton;

	public override void _Ready()
	{
		editor = GetNode<TextEdit>("/root/Window/VB/MainHB/Editor");
		preview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
		window = GetNode<Window>("/root/Window");
		editButton = GetNode<EditButton>("/root/Window/VB/BottomHB/EditButton");
		addButton = GetNode<AddButton>("/root/Window/VB/BottomHB/AddButton");
	}

	private void Pressed()
	{
		string path = window.GetFilePath();
		if(!path.Empty())
		{
			string fileContents = editor.Text;
			using (StreamWriter swriter = new StreamWriter(path, false))
			{
				swriter.Write(fileContents);
			}
			editor.Text = "";

			// Reapply text
			TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
			textPreview.SetTextFromFile(path);
			textPreview.Show();

			VSeparator seperator = GetNode<VSeparator>("/root/Window/VB/MainHB/VSeparator");
			seperator.Show();
		}
		else
		{
			AcceptDialog ad = GetNode<AcceptDialog>("/root/Window/Notifications/NoFile");
			ad.Show();
			GD.Print("Path is not set");
		}
		this.Hide();
		addButton.Show();
		editButton.Show();
	}
}



