using Godot;
using System;

public class TextPreview : RichTextLabel
{
	public void SetTextFromFile(String path)
	{
		this.Text = System.IO.File.ReadAllText(path);
	}

	public void ClearText()
	{
		this.Text = "";
	}
}
