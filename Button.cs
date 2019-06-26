using System;

class Button : IFocusable, IDrawable{
	
	private Action action;
	private Vector2i pos = new Vector2i();
	private Vector2i messagePos = new Vector2i(2, 2);
	private int width = 10, height = 5;
	private string message = "Button";
	
	string wallSimbol = "o";
	string focusedWallSimbol = "O";
	string unfocusedWallSimbol = "o";
	
	bool isFocused = false;
	
	public Button(Action action){
		this.action = action;
	}
	
	public void Focus(bool isFocused){
		this.isFocused = isFocused;
		if (isFocused)
			wallSimbol = focusedWallSimbol;
		else
			wallSimbol = unfocusedWallSimbol;
	}
	
	public string Message {
		get { return message; }
		set { 
			message = value; 
			width = message.Length + messagePos.x * 2; // front and back
		}
	}
	public int Width{
		get { return width; }
	}
	public int Height{
		get { return height; }
	}
	
	public Vector2i Position{
		get { return pos; }
		set { pos = value; }
	}
	
	public void Press(){
		action();
	}
	
	public void Draw(){
		for (int i = pos.x; i < width + pos.x; i++){
			Console.SetCursorPosition(i, pos.y);
			Console.Write(wallSimbol);
			Console.SetCursorPosition(i, pos.y + height - 1);
			Console.Write(wallSimbol);
		}
		for (int i = pos.y; i < height + pos.y; i++){
			Console.SetCursorPosition(pos.x, i);
			Console.Write(wallSimbol);
			Console.SetCursorPosition(pos.x + width - 1, i);
			Console.Write(wallSimbol);
		}
		Console.SetCursorPosition(pos.x + messagePos.x, pos.y + messagePos.y);
		Console.Write(message);
	}
}