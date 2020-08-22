namespace SearchNest.Model
{
    public class Document
    {
        public Document(string text, string fileName)
        {
            Text = text;
            FileName = fileName;
        }

        public string Text { get; set; }
        public string FileName { get; set; }
    }
}