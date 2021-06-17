using Bridge.Sys.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bridge.ModelTools
{

    public class EntityTypes
    {
     
        public List<Entity> Entities { get; set; }
    }

    public class Entity
    {
 

        public Entity()
        {
            Attributes = new List<Attribute>();
            References = new List<Reference>();
        }
        public string Name { get; set; }

        public string PluralName { get { return Name.Pluralize(); }  }
        public string Description { get; set; }
        [XmlIgnore]
        public bool IsRoot { get; internal set; }
        public string ParentEntity { get;  set; }
        public EntityRelationships RelationShip { get;  set; }


        public List<Attribute> Attributes { get; set; }

        public List<Reference> References { get; set; }




    }

    public class Attribute
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsVirtual { get; set; }

        public Attribute()
        { }

      
    }

    public class Reference
    {
        public Reference()
        {
            Type = Relationship.Equals(EntityRelationships.OneToOne) ? "long" : $"List<{Name}>";
        }
        public string Name { get; set; }
        public EntityRelationships Relationship { get; set; }
        public string Type { get; set; }
    }


    public enum EntityRelationships
    {
        OneToOne, OneToMany, ManyToMany
    }
}
