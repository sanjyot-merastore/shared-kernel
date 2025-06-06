using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;

public class ElasticsearchTemplatePusher(ElasticsearchClient client, Dictionary<string, string> fieldMappings, 
    string templateName = "app-logs-template", string indexPattern = "app-logs-*")
{
    public async Task PushAsync()
    {
        var exists = await client.Indices.ExistsIndexTemplateAsync(templateName);
        if (exists.Exists) return;

        var props = BuildDynamicProperties(fieldMappings);

        var request = new PutIndexTemplateRequest(templateName)
        {
            IndexPatterns = new[] { indexPattern },
            Template = new IndexTemplateMapping()
            {
                Mappings = new TypeMapping
                {
                    Dynamic = DynamicMapping.True,
                    Properties = props
                }
            }
        };

        var response = await client.Indices.PutIndexTemplateAsync(request);

        if (!response.IsValidResponse)
            throw new Exception($"Failed to create index template '{templateName}': {response.ElasticsearchServerError?.ToString()}");
    }

    private Properties BuildDynamicProperties(Dictionary<string, string> fieldMap)
    {
        var props = new Properties();

        foreach (var (name, type) in fieldMap)
        {
            IProperty property = type.ToLower() switch
            {
                "keyword" => new KeywordProperty(),
                "text" => new TextProperty(),
                "text.keyword" => new TextProperty
                {
                    Fields = new Properties()
                    {
                        { "keyword", new KeywordProperty() }
                    }
                },
                "integer" => new IntegerNumberProperty(),
                "long" => new LongNumberProperty(),
                "double" => new DoubleNumberProperty(),
                "boolean" => new BooleanProperty(),
                "date" => new DateProperty(),
                "ip" => new IpProperty(),
                "float" => new FloatNumberProperty(),
                "short" => new ShortNumberProperty(),
                "byte" => new ByteNumberProperty(),
                "scaled_float" => new ScaledFloatNumberProperty { ScalingFactor = 100 },
                "geo_point" => new GeoPointProperty(),
                "nested" => new NestedProperty(),
                _ => new ObjectProperty()
            };

            props[name] = property;
        }

        return props;
    }
}
