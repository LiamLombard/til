using Godot;
using System;
using System.IO;

public class AddButton : Button
{
	private TextEdit editor;
	private TextPreview preview;

	public override void _Ready()
	{
		editor = GetNode<TextEdit>("/root/Window/VB/MainHB/Editor");
		preview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
	}

	private void AddEntry()
	{
		Window w = GetNode<Window>("/root/Window");

		if(!w.path.Empty())
		{
			if(!editor.Text.Empty())
			{
				string fileContents = System.IO.File.ReadAllText(w.path);
				using (StreamWriter swriter = new StreamWriter(w.path, false))
				{
					fileContents = "* " + editor.Text + System.Environment.NewLine + fileContents;
					swriter.Write(fileContents);
				}

				// Reapply text
				TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
				textPreview.SetTextFromFile(w.path);

				// Clear editor
				editor.Text = "";
			}
		}
		else
		{
			AcceptDialog ad = GetNode<AcceptDialog>("/root/Window/Notifications/NoFile");
			ad.Show();
			GD.Print("Path is not set");
		}
	}
}

