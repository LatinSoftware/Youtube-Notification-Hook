using System.Xml.Serialization;

namespace Backoffice.Models
{
    [XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class Feed
    {
        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Links { get; set; } = new List<Link>();

        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public string Title { get; set; } = string.Empty;

        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public DateTime? Updated { get; set; }

        [XmlElement(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
        public Entry? Entry { get; set; }
    }

    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; } = string.Empty;
    }

    public class Entry
    {
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; } = string.Empty;

        [XmlElement(ElementName = "videoId", Namespace = "http://www.youtube.com/xml/schemas/2015")]
        public string VideoId { get; set; } = string.Empty;

        [XmlElement(ElementName = "channelId", Namespace = "http://www.youtube.com/xml/schemas/2015")]
        public string ChannelId { get; set; } = string.Empty;

        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public string Title { get; set; } = string.Empty;

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public Link? AlternateLink { get; set; } 

        [XmlElement(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
        public Author? Author { get; set; }

        [XmlElement(ElementName = "published", Namespace = "http://www.w3.org/2005/Atom")]
        public DateTime? Published { get; set; }

        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public DateTime? Updated { get; set; }
    }

    public class Author
    {
        [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Name { get; set; } = string.Empty;

        [XmlElement(ElementName = "uri", Namespace = "http://www.w3.org/2005/Atom")]
        public string Uri { get; set; } = string.Empty;
    }
}
