using System;
using System.Collections.Generic;
using System.Text;


namespace Bridge.ModelTools
{
    public static class CodeGenHelper
    {
        public static List<ModelDefinition> GenerateEntiityModel()
        {
            var metaModel = MetaModel.TryGetMetaModel();

            List<ModelDefinition> list = new List<ModelDefinition>();
            foreach (var entity in metaModel.EntityModel)
            {
                ModelDefinition model = new ModelDefinition() { Name = entity.Name };
                List<PropertyDefinition> props = new List<PropertyDefinition>();
                foreach (var attribute in entity.Attributes)
                {
                    props.Add(new PropertyDefinition(attribute.Name, attribute.Type));
                }
                model.Properties = props;
                list.Add(model);
            }
            return list;
        }

    
    }


    public class ModelDefinition
	{
		public string Name { get; set; }
		public List<PropertyDefinition> Properties { get; set; }
	}

	public class PropertyDefinition
	{
		public PropertyDefinition(string name, string type)
		{
			Name = name;
			Type = type;
		}

		public string Name { get; set; }
		public string Type { get; set; }
	}
}
