using NHibernate.Search.Attributes;

namespace Kona.Model {

    
    public class Descriptor {

        public virtual string Title { get; set; }
        [Field]
        public virtual string Body { get; set; }

    }
}
