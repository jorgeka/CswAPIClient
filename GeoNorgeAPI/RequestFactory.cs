﻿using System.Xml;
using www.opengis.net;

namespace GeoNorgeAPI
{
    public class RequestFactory
    {
        public GetRecordsType GetRecordsFreeTextSearch(string searchString, int startPosition = 1)
        {
            var getRecords = new GetRecordsType();
            getRecords.resultType = ResultType1.results;
            getRecords.startPosition = startPosition.ToString();
            getRecords.outputSchema = "csw:Record";

            var query = new QueryType();
            query.typeNames = new[] { new XmlQualifiedName("Record", "http://www.opengis.net/cat/csw/2.0.2") };
            var queryConstraint = new QueryConstraintType();
            queryConstraint.version = "1.1.0";
            queryConstraint.Item = new FilterType
                {
                    Items = new object[]
                        {
                            new PropertyIsLikeType
                            {
                                escapeChar = "\\",
                                singleChar = "_",
                                wildCard = "%",
                                PropertyName = new PropertyNameType { Text = new [] {"AnyText"} },
                                Literal = new LiteralType { Text = new [] { searchString } }
                            }  
                        },
                        ItemsElementName = new [] { ItemsChoiceType23.PropertyIsLike }
                };
            query.Constraint = queryConstraint;
            query.Items = new object[] { new ElementSetNameType { Value = ElementSetType.full } };
            getRecords.Item = query;

            return getRecords;
        }

        public GetRecordByIdType GetRecordById(string uuid)
        {
            GetRecordByIdType getRecordbyId = new GetRecordByIdType();
            getRecordbyId.service = "CSW";
            getRecordbyId.version = "2.0.2";
            getRecordbyId.outputSchema = "csw:IsoRecord";
            getRecordbyId.Id = new[] { uuid };
            getRecordbyId.ElementSetName = new ElementSetNameType { Value = ElementSetType.full };
            return getRecordbyId;
        }
    }
}
