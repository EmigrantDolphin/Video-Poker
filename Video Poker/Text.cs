using System;
using System.Collections.Generic;

namespace VideoPoker {
    class Text : IDrawable {
        Vector2 pos = new Vector2();
        string message;

        public Text() { }
        public Text(string message) {
            this.message = message;
        }
        public Text(Vector2 pos, string message) {
            this.pos = pos;
            this.message = message;
        }

        public string Message {
            get { return message; }
            set { message = value; }
        }
        public Vector2 Position {
            get { return pos; }
            set { pos = value; }
        }

        public void Draw() {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(message);
        }

    }
}
