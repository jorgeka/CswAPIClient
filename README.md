CswAPIClient
===========

C# API for communicating with an OGC CSW Service


Getting Started
===============

To install CswApiClient, run the following command in the Package Manager Console:

    PM> Install-Package CswApiClient
    

Example code
============

...

Security
========

...

SimpleMetadata
==============

The **MD_Metadata_Type** is quite large and complex since it is based on the ISO 19139 standard. This project contains a wrapper **SimpleMetadata** that does all the heavy lifting and manipulating of the **MD_Metadata_Type** object for you!

Example of use of **SimpleMetadata**

	SimpleMetadata simpleMetadata = SimpleMetadata.CreateDataset();
	simpleMetadata.Title = "This is my dataset!";
	simpleMetadata.Abstract = "This is the abstract telling you everything you need to know about this dataset.";
	simpleMetadata.ContactPublisher = new SimpleContact
	{ 
	    Name = "John Smith",
      Email = "nothing@example.com",
      Organization = "My organization",
      Role = "publisher" 
	};


**SimpleMetadata** now contains a **MD_Metadata_Type** that looks like this when serialized to XML:

    <?xml version="1.0" encoding="utf-8"?>
    <gmd:MD_Metadata xmlns:gml="http://www.opengis.net/gml/3.2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:csw="http://www.opengis.net/cat/csw/2.0.2" xmlns:gco="http://www.isotc211.org/2005/gco" xmlns:srv="http://www.isotc211.org/2005/srv" xmlns:gts="http://www.isotc211.org/2005/gts" xmlns:gmd="http://www.isotc211.org/2005/gmd">
      <gmd:hierarchyLevel>
        <gmd:MD_ScopeCode codeList="http://www.isotc211.org/2005/resources/Codelist/ML_gmxCodelists.xml#MD_ScopeCode" codeListValue="dataset" />
      </gmd:hierarchyLevel>
      <gmd:identificationInfo>
        <gmd:MD_DataIdentification>
          <gmd:citation>
            <gmd:CI_Citation>
              <gmd:title>
                <gco:CharacterString>This is my dataset!</gco:CharacterString>
              </gmd:title>
            </gmd:CI_Citation>
          </gmd:citation>
          <gmd:abstract>
            <gco:CharacterString>This is the abstract telling you everything you need to know about this dataset.</gco:CharacterString>
          </gmd:abstract>
          <gmd:pointOfContact>
            <gmd:CI_ResponsibleParty>
              <gmd:individualName>
                <gco:CharacterString>John Smith</gco:CharacterString>
              </gmd:individualName>
              <gmd:organisationName>
                <gco:CharacterString>My organization</gco:CharacterString>
              </gmd:organisationName>
              <gmd:contactInfo>
                <gmd:CI_Contact>
                  <gmd:address>
                    <gmd:CI_Address>
                      <gmd:electronicMailAddress>
                        <gco:CharacterString>nothing@example.com</gco:CharacterString>
                      </gmd:electronicMailAddress>
                    </gmd:CI_Address>
                  </gmd:address>
                </gmd:CI_Contact>
              </gmd:contactInfo>
              <gmd:role>
                <gmd:CI_RoleCode codeList="http://standards.iso.org/ittf/PubliclyAvailableStandards/ISO_19139_Schemas/resources/Codelist/ML_gmxCodelists.xml#CI_RoleCode" codeListValue="publisher" />
              </gmd:role>
            </gmd:CI_ResponsibleParty>
          </gmd:pointOfContact>
        </gmd:MD_DataIdentification>
      </gmd:identificationInfo>
    </gmd:MD_Metadata>


The **MD_Metadata_Type** can be inserted into CswApi by using the API like this:

    var transaction = _cswApi.MetadataInsert(simpleMetadata.GetMetadata());
    

The **SimpleMetadata** wrapper has methods for all fields that is required to be compliant with [INSPIRE](http://inspire.ec.europa.eu/).
