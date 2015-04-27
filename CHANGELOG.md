# Change Log
All notable changes to this project will be documented in this file.

## [6.1.0] - 2015-4-27
###Added
- Added support for sending via API keys in addition to credentials. Pass an API Key string to the Web transport constructor.

## [6.0.1] - 2015-4-24
###Fixed
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
