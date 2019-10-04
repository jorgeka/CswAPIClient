using NUnit.Framework;
using www.opengis.net;
using CswApiClient;
using System;
using System.Collections.Generic;

namespace CswApiClient.Tests
{
    
    public class CswTests
    {
        CswApi _cswApi;

        [SetUp]
        public void Init()
        {
            _cswApi = new CswApiClient.CswApi();
        }

        [Test]
        public void ShouldReturnRecordsWhenRunningASimpleSearch()
        {
            var result = _cswApi.Search("wms");

            Assert.Greater(int.Parse(result.numberOfRecordsMatched), 0, "A search on 'wms' should return records.");
        }
        
        [Test]
        public void ShouldReturnSingleIsoRecord()
        {
            MD_Metadata_Type record = _cswApi.GetRecordByUuid("63c672fa-e180-4601-a176-6bf163e0929d"); // Matrikkelen WMS
            
            Assert.NotNull(record, "Record does not exist.");
        }

        [Test]
        public void ShouldReturnRecordsWhenSearchingWithOrganisationName()
        {
            var result = _cswApi.SearchWithOrganisationName("%Kartverket%");

            Assert.Greater(int.Parse(result.numberOfRecordsMatched), 0, "An organization name search on '%Kartverket%' should return lots of records.");
        }

        [Test]
        public void ShouldReturnRecordsWhenSearchingWithOrganisationNameIncludingWhitespace()
        {
            var result = _cswApi.SearchWithOrganisationName("Norsk institutt for bioøkonomi");

            Assert.Greater(int.Parse(result.numberOfRecordsMatched), 0, "An organization name search on 'Norsk institutt for skog og landskap' should return lots of records.");
        }
        [Test]
        public void ShouldReturnServicesFromKartverket()
        {
            var filters = new object[]
                {
                    
                    new BinaryLogicOpType()
                        {
                            Items = new object[]
                                {
                                    new PropertyIsLikeType
                                    {
                                        escapeChar = "\\",
                                        singleChar = "_",
                                        wildCard = "%",
                                        PropertyName = new PropertyNameType {Text = new[] {"OrganisationName"}},
                                        Literal = new LiteralType {Text = new[] { "%Kartverket%" }}
                                    },
                                    new PropertyIsLikeType
                                    {
                                        PropertyName = new PropertyNameType {Text = new[] {"Type"}},
                                        Literal = new LiteralType {Text = new[] { "service" }}
                                    }
                                },
                                ItemsElementName = new ItemsChoiceType22[]
                                    {
                                        ItemsChoiceType22.PropertyIsLike, ItemsChoiceType22.PropertyIsLike, 
                                    }
                        },
                    
                };

            var filterNames = new ItemsChoiceType23[]
                {
                    ItemsChoiceType23.And
                };

            var result = _cswApi.SearchWithFilters(filters, filterNames);

            Assert.Greater(int.Parse(result.numberOfRecordsMatched), 0, "Should have return more than zero datasets from Kartverket.");
        }

        [Test]
        public void ShouldReturnRecordsSpecifiedNumberOfRecords()
        {
            int numberOfRecords = 30;
            var result = _cswApi.Search("data", 1, numberOfRecords);

            Assert.Greater(int.Parse(result.numberOfRecordsMatched), 0, "A search on 'data' should return records.");
            Assert.AreEqual(numberOfRecords, int.Parse(result.numberOfRecordsReturned), "Should have returned 30 records");
        }

        [Test]
        public void ShouldSearchAndReturnIsoRecords()
        {
            var result = _cswApi.SearchIso("data");

            Assert.Greater(int.Parse(result.numberOfRecordsMatched), 0);

            MD_Metadata_Type md = result.Items[0] as MD_Metadata_Type;
            Assert.NotNull(md);
        }


        [Test]
        public void ShouldParseCswTransactionAfterUpdateWithoutNullReferenceOnMissingIdentifiers()
        {
            MetadataTransaction transaction = RequestRunner.ParseCswTransactionResponse("<csw:TransactionResponse xmlns:csw=\"http://www.opengis.net/cat/csw/2.0.2\"><csw:TransactionSummary><csw:totalInserted>0</csw:totalInserted><csw:totalUpdated>1</csw:totalUpdated><csw:totalDeleted>0</csw:totalDeleted></csw:TransactionSummary></csw:TransactionResponse>");

            Assert.AreEqual("1", transaction.TotalUpdated);
        }

        /*
        [Test]
        public void InsertMetadata()
        {
            _CswApiClient = new CswApiClient("","","https://www.CswApiClient.no/geonetworkbeta/");

            MD_Metadata_Type metadata = MetadataExample.CreateMetadataExample();
            metadata.fileIdentifier = new CharacterString_PropertyType { CharacterString = Guid.NewGuid().ToString() };

            var transaction = _CswApiClient.MetadataInsert(metadata, new Dictionary<string, string> { {"CswApiClientUsername", "blabla"} });

            Assert.NotNull(transaction);
            Assert.AreEqual("1", transaction.TotalInserted);

            Console.WriteLine(transaction.Identifiers);
        }
        
        [Test]
        public void UpdateMetadata()
        {
            _CswApiClient = new CswApiClient("", "", "http://beta.CswApiClient.no/geonetwork/");

            MD_Metadata_Type metadata = MetadataExample.CreateMetadataExample();

            _CswApiClient.MetadataUpdate(metadata);
        }

        [Test]
        public void DeleteMetadata()
        {
            _CswApiClient = new CswApiClient("", "", "http://beta.CswApiClient.no/geonetwork/");
            _CswApiClient.MetadataDelete("bcffba00-5396-4f81-ad65-d34d8771eab4");
        }
        */
    }
}
