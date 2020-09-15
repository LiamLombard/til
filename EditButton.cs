using Godot;
using System;
using System.IO;

public class EditButton : Button
{
	private bool editing = false;

	private TextEdit editor;
	private TextPreview preview;
	private Window window;

	public override void _Ready()
	{
		editor = GetNode<TextEdit>("/root/Window/VB/MainHB/Editor");
		preview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
		window = GetNode<Window>("/root/Window");
	}

	private void Pressed()
	{
		if(editing)
		{
			CloseUp();
		}
		else
		{
			PrepareEditor();
		}
		editing = !editing;
	}

	private void PrepareEditor()
	{
		if(!window.path.Empty())
		{
			string fileContents = System.IO.File.ReadAllText(window.path);
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
	}

	private void CloseUp()
	{
		if(!window.path.Empty())
		{
			string fileContents = editor.Text;
			using (StreamWriter swriter = new StreamWriter(window.path, false))
			{
				swriter.Write(fileContents);
			}
			editor.Text = "";

			// Reapply text
			TextPreview textPreview = GetNode<TextPreview>("/root/Window/VB/MainHB/TextPreview");
			textPreview.SetTextFromFile(window.path);
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
	}
}



