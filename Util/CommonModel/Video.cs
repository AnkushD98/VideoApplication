namespace Util.CommonModel
{
    public class Video
    {
        int id;
        string title;
        string description;
        Uri path;
        string length;

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public Uri Path { get => path; set => path = value; }
        public string Length { get => length; set => length = value; }

        public Video(int id, string title, string description, Uri path, string length)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Path = path;
            this.Length = length;
        }

    }   
}
