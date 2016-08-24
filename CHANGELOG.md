# Change Log
All notable changes to this project will be documented in this file.

## [8.0.4] - 2016-08-24 ##
### Added
- Table of Contents in the README
- Added a [USE_CASES.md](https://github.com/sendgrid/sendgrid-csharp/blob/master/USE_CASES.md) section, with the first use case example for transactional templates

## [8.0.3] - 2016-08-17 ##
## Fixed
- [Issue #297](https://github.com/sendgrid/sendgrid-csharp/issues/297): Don't Include Empty Objects in JSON Request Body
- If you clear out the values of the to, bcc or cc lists in the personalization objects, they will no longer be included in the JSON request body

## [8.0.2] - 2016-08-01 ##
## Fixed
- [Issue #273](https://github.com/sendgrid/sendgrid-csharp/issues/273): Disable (or set) tracking
- Now, settings set to false will generate the correct JSON
- Big thanks to [Pontus Öwre](https://github.com/owre) for the pull!

## [8.0.1] - 2016-07-25 ## 
### Added 
- [Troubleshooting](https://github.com/sendgrid/sendgrid-csharp/blob/master/TROUBLESHOOTING.md) section 

## [8.0.0] - 2016-07-22 ## 
## BREAKING CHANGE 
- updated dependency on [SendGrid.CSharp.HTTP.Client](https://github.com/sendgrid/csharp-http-client/releases/tag/v3.0.0), which had a breaking change 
- Fixes [issue #259](https://github.com/sendgrid/sendgrid-csharp/issues/259) 
- the async behavior in the HTTP client has changed, as we don’t block on .Result anymore  
- Updated USAGE, examples and README to demonstrate await usage 

## [7.1.1] - 2016-07-20 ## 
### Added 
- README updates 
- Update introduction blurb to include information regarding our forward path 
- Update the v3 /mail/send example to include non-helper usage 
- Update the generic v3 example to include non-fluent interface usage 

## [7.1.0] - 2016-07-19
### Added
- Update [csharp-http-client](https://github.com/sendgrid/csharp-http-client) dependency to [support setting a WebProxy](https://github.com/sendgrid/csharp-http-client/releases/tag/v2.0.7)

## [7.0.7] - 2016-07-18
### Fixed
- Fix for [issue #256](https://github.com/sendgrid/sendgrid-csharp/issues/256): SendGrid v3 and HTML emails - Creates bad Json 
- Updated dependency to SendGrid.Csharp.HTTP.Client to 2.0.6
- Updated dependency to JSON.NET to 9.0.1 in the Example and SendGrid projects
- Removed dependencies to SendGrid.CSharp.HTTP.Client and SendGrid.SmtpApi from the Example and UnitTests projects as they are not needed
- Update examples, unit tests and USAGE.md to pass in valid JSON
- Thanks to [Gunnar Liljas](https://github.com/gliljas) for helping find the root cause quickly! 

## [7.0.6] - 2016-07-12
### Added
- Update docs, unit tests and examples to include Sender ID

## [7.0.5] - 2016-07-08
### Added
- Tests now mocked automatically against [prism](https://stoplight.io/prism/)

## [7.0.4] - 2016-07-05
### Added
- Accept: application/json header per https://sendgrid.com/docs/API_Reference/Web_API_v3/How_To_Use_The_Web_API_v3/requests.html

### Updated
- Content based on our updated [Swagger/OAI doc](https://github.com/sendgrid/sendgrid-oai)

## [7.0.3] - 2016-06-28
### Fixed
- Send mail fails with BadRequest when apostrophe used in sender name: https://github.com/sendgrid/sendgrid-csharp/issues/232

## [7.0.2] - 2016-06-16
### Fixed
- Async broken in library, causing deadlocks and responses not returning in non-console apps: https://github.com/sendgrid/sendgrid-csharp/issues/235

## [7.0.1] - 2016-06-15
### Fixed
- Hard-coded subject in mail helper: https://github.com/sendgrid/sendgrid-csharp/issues/234
- Thanks [digime99](https://github.com/digime99)!

## [7.0.0] - 2016-06-13
### Added
- Breaking change to support the v3 Web API
- New HTTP client
- v3 Mail Send helper

## [6.3.4] - 2015-12-15
### Added
- Implemented the global stats /asm/stats endpoint [GET]

## [6.3.3] - 2015-12-14
### Added
- Implemented the global suppressions /asm/suppressions/global endpoint [GET, POST, DELETE]

## [6.3.2] - 2015-12-11
### Added
- Implemented the suppressions /asm/groups/:group_id/suppressions endpoint [GET, POST, DELETE]

## [6.3.1] - 2015-12-10
### Added
- Implemented the unsubscribe groups /asm/groups endpoint [GET, POST, DELETE]

## [6.3.0] - 2015-11-24
### Added
- Send emails using API Key

## [6.2.0] - 2015-11-18
### Added
- Added support for using the Web API v3 endpoints
- Implemented the api_keys endpoint [GET, POST, PATCH, DELETE]

## [6.1.0] - 2015-4-27
### Added
- Added support for sending via API keys in addition to credentials. Pass an API Key string to the Web transport constructor

## [6.0.1] - 2015-4-24
### Fixed
- Fixed the endpoint URL. (⌒_⌒;)

## [6.0.0] - 2015-4-22
The only breaking change in this release is the removal of the non-async
Deliver method. All other changes are backwards compatible.

### Added
- AddSection() method for SMTP API substitution sections (thanks @awwa)
- EmbedStreamImage() to embed directly from a memory stream with no disk
  i/o (thanks @twilly86)
- SendToSink bool that when true will send all emails to the test sink
  (thanks @lukasz-lysik)
- SetSendAt() and SetSendEachAt() methods for scheduled sends
- SetIpPool() method for using IP pools
- SetAsmGroupId() method for using suppression groups

### Changed
- Refactored error handling (thanks @HowardvanRooijen)
- Removed non-async Deliver() method as it was mixing sync and async
  code.

### Fixed
- Invalid Protocol Exception in Mono due to the way the endpoint URL was
  being assigned (thanks @mdymel, @rbarinov)
- Connections were not being reused. This was causing degraded
  performance with multiple threads open. Performance should be much
better. (with help from @gatesvp)

## [5.1.0] - 2015-1-26
### Added
- This changelog.
- `Web` transport constructor that accepts a `TimeSpan` to specify HTTP timeout
- Null values in header will now result in a `ArgumentNullException`

### Changed
- Updated to SendGrid.SmtpApi 1.2.0, which means Unicode in header values will work properly.

### Fixed
- Removed redundant status code check that was throwing unhelpful errors
- Unicode in header values will now work properly
