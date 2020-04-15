using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class DialogueParser : MonoBehaviour
{
    public string dialogueFile;
    public string commentFile;
    List<DialogueLine> lines;
    List<CommentLine> lineComment;
    //public List<Sprite> images;
    public Sprite[] images;
    struct DialogueLine
    {
        public string name;
        public string content;
        public int pose;
        public string position;
        public bool inScene;
        public bool decision;
        public bool commentary;

        public DialogueLine(string n, string c, int p, string pos, bool inSc, bool dec, bool comment)
        {
            name = n;
            content = c;
            pose = p;
            position = pos;
            inScene = inSc;
            decision = dec;
            commentary = comment;
        }
    }

    
    struct CommentLine
    {
        public string commentContent;
        public bool next;
        public CommentLine(string co, bool nx)
        {
            commentContent = co;
            next = nx;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lines = new List<DialogueLine>(); //instanciar lista
        lineComment = new List<CommentLine>();

        //LoadDialogue(file);
        LoadDialogue(dialogueFile +".txt");
        LoadComments(commentFile +".txt");
        //images = new List<Sprite>();
        LoadImages();
    }

    public string GetName(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return lines[lineNumber].name;
        return "";
    }
    public string GetContent(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return lines[lineNumber].content;
        return "";
    }
    public Sprite GetPose(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return images[lines[lineNumber].pose];
        return null;
    }
    public string GetPosition(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return lines[lineNumber].position;

        return ""; 
    }
    public bool GetInScene(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return lines[lineNumber].inScene;

        return true;
    }
    public bool GetDecision(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return lines[lineNumber].decision;

        return false;
    }
    public bool GetCommentary(int lineNumber)
    {
        if (lineNumber < lines.Count)
            return lines[lineNumber].commentary;

        return false;
    }
    public string GetCommentContent(int lineNumber)
    {
        if (lineNumber < lineComment.Count)
            return lineComment[lineNumber].commentContent;

        return "";
    }
    public bool GetNextComment(int lineNumber)
    {
        if (lineNumber < lineComment.Count)
            return lineComment[lineNumber].next;

        return false;
    }

    void LoadDialogue(string filename)
    {
        string file = "Assets/Resources/Dialogues/" + filename;
        string line;
        StreamReader r = new StreamReader(file);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    string[] line_values = SplitCsvLine(line);
                    DialogueLine line_entry = new DialogueLine(line_values[0], line_values[1], int.Parse(line_values[2]), line_values[3], bool.Parse(line_values[4]), bool.Parse(line_values[5]), bool.Parse(line_values[6]));
                    lines.Add(line_entry);
                }
            }
            while (line != null);
            r.Close();
        }
    }
    void LoadComments(string filename)
    {
        string file = "Assets/Resources/Comments/" + filename;
        string line;
        StreamReader r = new StreamReader(file);

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    string[] line_values = SplitCsvLine(line);
                    CommentLine line_entry = new CommentLine(line_values[0], bool.Parse(line_values[1]));
                    lineComment.Add(line_entry);
                }
            }
            while (line != null);
            r.Close();
        }
    }
    void LoadImages()
    {
        for(int i = 0; i < lines.Count; i++)
        {
            /*string imageName = lines[i].name;
            Sprite image = (Sprite)Resources.Load(imageName, typeof(Sprite));
            if(!images.Contains(image))
            {
                images.Add(image);
            }*/
            
        }
        images = Resources.LoadAll<Sprite>("SpritesGame");
    }
    //https://answers.unity.com/questions/144200/are-there-any-csv-reader-for-unity3d-without-needi.html?sort=oldest
    string[] SplitCsvLine(string line)
    {
        string pattern = @"
     # Match one value in valid CSV string.
     (?!\s*$)                                      # Don't match empty last value.
     \s*                                           # Strip whitespace before value.
     (?:                                           # Group for value alternatives.
       '(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)'       # Either $1: Single quoted string,
     | ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)""   # or $2: Double quoted string,
     | (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*)    # or $3: Non-comma, non-quote stuff.
     )                                             # End group of value alternatives.
     \s*                                           # Strip whitespace after value.
     (?:,|$)                                       # Field ends on comma or EOS.
     ";
        string[] values = (from Match m in Regex.Matches(line, pattern,
                        RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | 
                        RegexOptions.Multiline) select m.Groups[1].Value).ToArray();
        return values;
    }
}
