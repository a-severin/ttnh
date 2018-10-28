using System.Xml.Linq;

namespace NameHelper
{
    public interface IConfigInitializer
    {
        void Init(XElement xConfig);
    }
}