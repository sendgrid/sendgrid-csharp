# Change Log
All notable changes to this project will be documented in this file.

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
