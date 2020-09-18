using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
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
				string date = DateTime.Now.ToString("dd.MM.yy");
				bool found = false;
				LinkedList<string> fileContents = new LinkedList<string>();

				foreach(string line in System.IO.File.ReadLines(w.path))
				{
					GD.Print($"{date} in {line}? -> {line.Contains(date)}");
					if(!found && line.Contains(date))
					{
						found = true;

						fileContents.AddLast(line);
						fileContents.AddLast($"* {editor.Text}");
						continue;
					}
					fileContents.AddLast(line);
				}

				if(!found)
				{
					// Have to add in reverse order because we're adding to the top
					fileContents.AddFirst($"* {editor.Text}");
					fileContents.AddFirst($"# {date}");
				}

				using (StreamWriter swriter = new StreamWriter(w.path, false))
				{
					foreach (string line in fileContents)
					{
						swriter.WriteLine(line);
					}
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

