using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Bridge.ModelTools.MetaModel;

namespace Bridge.ModelTools
{
    public interface IXmlLoader
    {
        ICollection<Entity> Load(string SourceXML);
    }
    class EntityXmlLoader : IXmlLoader
    {
        public ICollection<Entity> Load(string SourceXML)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(SourceXML);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.ChildNodes[0].ChildNodes;

            ICollection<Entity> entities = new List<Entity>();

            foreach (XmlElement Entity in nodes)
            {
                Entity e = new Entity
                {
                    Name = Entity.GetAttribute("Name"),
                    Description = Entity.GetAttribute("Description"),
                    IsRoot = string.IsNullOrEmpty(Entity.GetAttribute("ParentEntity")) ? true : false,

                };

                AddAttributesToEntity(Entity, e);

                if (!e.IsRoot)
                {
                    string parentEntityName = Entity.GetAttribute("ParentEntity");
                    var parentEntity = entities.Where(x => x.Name == parentEntityName).FirstOrDefault();
                    if (parentEntity == null)
                    {
                        throw new Exception($"Invalid parent entity  for entity : {e.Name}");
                    }
                    AddNavigationPropertyToParentEntity(Entity, e, parentEntity);
                    AddNavigationPropertyToCurrentEntity(e, parentEntityName);
                }


                entities.Add(e);

            }

            return entities;
        }

        private static void AddAttributesToEntity(XmlElement Entity, Entity e)
        {
            foreach (XmlElement collectionChildNode in Entity.ChildNodes)
            {
                if (collectionChildNode.Name == "Entity.Attributes")
                {
                    var attributeElements = collectionChildNode.ChildNodes;
                    foreach (XmlElement attribute in attributeElements)
                    {

                        var Name = attribute.GetAttribute("Name");
                        var Type = attribute.GetAttribute("Type");
                       
                        Attribute att = new Attribute() { Name= Name, Type = Type};
                        e.Attributes.Add(att);
                    }

                }
            }
        }

        private static void AddNavigationPropertyToParentEntity(XmlElement Entity, Entity e, Entity parentEntity)
        {
            parentEntity.References.Add(new Reference() { Name = e.Name, Relationship = Entity.GetAttribute("ParentEntity") == "OneToOne" ? EntityRelationships.OneToOne : EntityRelationships.OneToMany});
        }

        private void AddNavigationPropertyToCurrentEntity(Entity e,string parentEntityName)
        {
            e.Attributes.Add(new Attribute() { Name = parentEntityName, Type = parentEntityName, IsVirtual = true });
            e.Attributes.Add(new Attribute() { Name = parentEntityName.GetForeignKeyId(), Type = "long" });
        }
    }
}
