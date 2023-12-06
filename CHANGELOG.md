# Change Log
All notable changes to this project will be documented in this file.

[2023-12-06] Version 9.29.1
---------------------------
**Library - Fix**
- [PR #1191](https://github.com/sendgrid/sendgrid-csharp/pull/1191): update deployment pipeline to add signing step. Thanks to [@shrutiburman](https://github.com/shrutiburman)!


[2023-12-01] Version 9.29.0
---------------------------
**Library - Feature**
- [PR #1190](https://github.com/sendgrid/sendgrid-csharp/pull/1190): Add data residency for eu and global regions. Thanks to [@shrutiburman](https://github.com/shrutiburman)!


[2022-08-10] Version 9.28.1
---------------------------
**Library - Docs**
- [PR #1181](https://github.com/sendgrid/sendgrid-csharp/pull/1181): Example of adding a WebProxy using DI. Thanks to [@mortenbock](https://github.com/mortenbock)!

**Library - Fix**
- [PR #1180](https://github.com/sendgrid/sendgrid-csharp/pull/1180): Use httpErrorAsException when passed as parameter to SendGridClient constructor. Thanks to [@mortenbock](https://github.com/mortenbock)!

**Library - Miscellaneous**
- [PR #1178](https://github.com/sendgrid/sendgrid-csharp/pull/1178): bump Newtonsoft.Json from 11.0.2 to 13.0.1 in /examples/inbound-webhook-handler/Tests/Inbound.Tests. Thanks to [@dependabot](https://github.com/dependabot)!

**Library - Test**
- [PR #1179](https://github.com/sendgrid/sendgrid-csharp/pull/1179): Adding misc as PR type. Thanks to [@rakatyal](https://github.com/rakatyal)!


[2022-05-18] Version 9.28.0
---------------------------
**Library - Docs**
- [PR #1176](https://github.com/sendgrid/sendgrid-csharp/pull/1176): Update to align with SendGrid Support. Thanks to [@garethpaul](https://github.com/garethpaul)!

**Library - Chore**
- [PR #1174](https://github.com/sendgrid/sendgrid-csharp/pull/1174): Security upgrade Newtonsoft.Json from 9.0.1 to 13.0.1. Thanks to [@svcprodsec-sendgrid](https://github.com/svcprodsec-sendgrid)!
- [PR #1173](https://github.com/sendgrid/sendgrid-csharp/pull/1173): Security upgrade Newtonsoft.Json from 9.0.1 to 13.0.1. Thanks to [@svcprodsec-sendgrid](https://github.com/svcprodsec-sendgrid)!
- [PR #1175](https://github.com/sendgrid/sendgrid-csharp/pull/1175): Security upgrade Newtonsoft.Json from 10.0.3 to 13.0.1. Thanks to [@svcprodsec-sendgrid](https://github.com/svcprodsec-sendgrid)!

**Library - Feature**
- [PR #1169](https://github.com/sendgrid/sendgrid-csharp/pull/1169): Add support for multiple Reply-Tos. Thanks to [@RyanFlemingOlo](https://github.com/RyanFlemingOlo)!


[2022-03-23] Version 9.27.0
---------------------------
**Library - Fix**
- [PR #1168](https://github.com/sendgrid/sendgrid-csharp/pull/1168): fallback to private body and headers for response deserialization. Thanks to [@childish-sambino](https://github.com/childish-sambino)!

**Library - Feature**
- [PR #1153](https://github.com/sendgrid/sendgrid-csharp/pull/1153): Added mail settings attributes for bypass_spam_management, bypass_bou…. Thanks to [@asos-vinodpatil](https://github.com/asos-vinodpatil)!


[2022-03-09] Version 9.26.0
---------------------------
**Library - Feature**
- [PR #1156](https://github.com/sendgrid/sendgrid-csharp/pull/1156): make RetriableServerErrorStatusCodes public. Thanks to [@maxkoshevoi](https://github.com/maxkoshevoi)!

**Library - Chore**
- [PR #1166](https://github.com/sendgrid/sendgrid-csharp/pull/1166): push Datadog Release Metric upon deploy success. Thanks to [@eshanholtz](https://github.com/eshanholtz)!

**Library - Fix**
- [PR #1151](https://github.com/sendgrid/sendgrid-csharp/pull/1151): Use the private body and header datamembers for DeserializeResponseBodyAsync and DeserializeResponseHeaders. Thanks to [@Wind010](https://github.com/Wind010)!


[2022-02-09] Version 9.25.3
---------------------------
**Library - Chore**
- [PR #1161](https://github.com/sendgrid/sendgrid-csharp/pull/1161): add gh release to workflow. Thanks to [@shwetha-manvinkurke](https://github.com/shwetha-manvinkurke)!
- [PR #1158](https://github.com/sendgrid/sendgrid-csharp/pull/1158): merge test and deploy workflows. Thanks to [@shwetha-manvinkurke](https://github.com/shwetha-manvinkurke)!


[2022-01-12] Version 9.25.2
---------------------------
**Library - Chore**
- [PR #1152](https://github.com/sendgrid/sendgrid-csharp/pull/1152): update license year. Thanks to [@JenniferMah](https://github.com/JenniferMah)!


[2021-12-01] Version 9.25.1
---------------------------
**Library - Chore**
- [PR #1141](https://github.com/sendgrid/sendgrid-csharp/pull/1141): [Snyk] Fix for 6 vulnerabilities. Thanks to [@svcprodsec-sendgrid](https://github.com/svcprodsec-sendgrid)!
- [PR #1142](https://github.com/sendgrid/sendgrid-csharp/pull/1142): migrate to GitHub Actions. Thanks to [@eshanholtz](https://github.com/eshanholtz)!


[2021-11-17] Version 9.25.0
---------------------------
**Library - Chore**
- [PR #1140](https://github.com/sendgrid/sendgrid-csharp/pull/1140): bump ecdsa-dotnet version. Thanks to [@JenniferMah](https://github.com/JenniferMah)!

**Library - Feature**
- [PR #1137](https://github.com/sendgrid/sendgrid-csharp/pull/1137): add tests & use case for From personalization. Thanks to [@beebzz](https://github.com/beebzz)!


[2021-10-18] Version 9.24.4
---------------------------
**Library - Docs**
- [PR #1134](https://github.com/sendgrid/sendgrid-csharp/pull/1134): improve signed webhook event validation docs. Thanks to [@shwetha-manvinkurke](https://github.com/shwetha-manvinkurke)!


[2021-09-22] Version 9.24.3
---------------------------
**Library - Docs**
- [PR #1133](https://github.com/sendgrid/sendgrid-csharp/pull/1133): enhancing intellisense to provide parameter limits in the description…. Thanks to [@jmounts234](https://github.com/jmounts234)!


[2021-08-11] Version 9.24.2
---------------------------
**Library - Docs**
- [PR #1126](https://github.com/sendgrid/sendgrid-csharp/pull/1126): Updated broken Readme.md links. Thanks to [@DanielHolland](https://github.com/DanielHolland)!


[2021-07-14] Version 9.24.1
---------------------------
**Library - Docs**
- [PR #1123](https://github.com/sendgrid/sendgrid-csharp/pull/1123): fix mistake in README.md. Thanks to [@hakuna-matata-in](https://github.com/hakuna-matata-in)!


[2021-06-21] Version 9.24.0
---------------------------
**Library - Chore**
- [PR #1116](https://github.com/sendgrid/sendgrid-csharp/pull/1116): bump SendGrid.Extensions.DependencyInjection from 1.0.0 to 1.0.1. Thanks to [@akunzai](https://github.com/akunzai)!

**Library - Feature**
- [PR #1115](https://github.com/sendgrid/sendgrid-csharp/pull/1115): add `SendGridMessage` deserialization. Thanks to [@jlawcordova](https://github.com/jlawcordova)!


[2021-05-19] Version 9.23.2
---------------------------
**Library - Fix**
- [PR #1109](https://github.com/sendgrid/sendgrid-csharp/pull/1109): Strong name signature could not be verified. Thanks to [@akunzai](https://github.com/akunzai)!


[2021-05-05] Version 9.23.1
---------------------------
**Library - Chore**
- [PR #1105](https://github.com/sendgrid/sendgrid-csharp/pull/1105): bump System.Net.Http from 4.0.0 to 4.3.4 in /ExampleNet45ASPNetProject/SendGrid.ASPSamples. Thanks to [@dependabot](https://github.com/dependabot)!
- [PR #1106](https://github.com/sendgrid/sendgrid-csharp/pull/1106): bump System.Net.Http from 4.3.3 to 4.3.4 in /ExampleNet45ASPNetProject/SendGrid.ASPWebFormsSamples. Thanks to [@dependabot](https://github.com/dependabot)!


[2021-04-21] Version 9.23.0
---------------------------
**Library - Feature**
- [PR #578](https://github.com/sendgrid/sendgrid-csharp/pull/578): Add Permissions API support. Thanks to [@teabaggs](https://github.com/teabaggs)!
- [PR #1102](https://github.com/sendgrid/sendgrid-csharp/pull/1102): Add From Email to Personalization. Thanks to [@Stonebkfly](https://github.com/Stonebkfly)!

**Library - Chore**
- [PR #1100](https://github.com/sendgrid/sendgrid-csharp/pull/1100): bump ecdsa-dotnet version. Thanks to [@JenniferMah](https://github.com/JenniferMah)!


[2020-12-16] Version 9.22.0
---------------------------
**Library - Feature**
- [PR #1077](https://github.com/sendgrid/sendgrid-csharp/pull/1077): add IsSuccessStatusCode. Thanks to [@shoter](https://github.com/shoter)!


[2020-11-18] Version 9.21.2
---------------------------
**Library - Fix**
- [PR #1064](https://github.com/sendgrid/sendgrid-csharp/pull/1064): Allow SendGridMessage serialization to optionally bypass defaults. Thanks to [@duncanchung](https://github.com/duncanchung)!


[2020-11-05] Version 9.21.1
---------------------------
**Library - Docs**
- [PR #1059](https://github.com/sendgrid/sendgrid-csharp/pull/1059): fix sender email object description in MailHelper.cs. Thanks to [@makma](https://github.com/makma)!


[2020-08-19] Version 9.21.0
---------------------------
**Library - Chore**
- [PR #1036](https://github.com/sendgrid/sendgrid-csharp/pull/1036): simplify framework dependencies. Thanks to [@childish-sambino](https://github.com/childish-sambino)!
- [PR #1032](https://github.com/sendgrid/sendgrid-csharp/pull/1032): update GitHub branch references to use HEAD. Thanks to [@thinkingserious](https://github.com/thinkingserious)!

**Library - Feature**
- [PR #789](https://github.com/sendgrid/sendgrid-csharp/pull/789): Add dockerized email inbound webhook consumer example. Thanks to [@KoditkarVedant](https://github.com/KoditkarVedant)!

**Library - Docs**
- [PR #757](https://github.com/sendgrid/sendgrid-csharp/pull/757): Run *.md documents through Grammer.ly. Thanks to [@danimart1991](https://github.com/danimart1991)!


[2020-08-05] Version 9.20.0
---------------------------
**Library - Feature**
- [PR #812](https://github.com/sendgrid/sendgrid-csharp/pull/812): Add dockerized event webhook consumer example. Thanks to [@KoditkarVedant](https://github.com/KoditkarVedant)!


[2020-07-22] Version 9.19.0
---------------------------
**Library - Feature**
- [PR #1030](https://github.com/sendgrid/sendgrid-csharp/pull/1030): allow the `RetryDelegatingHandler` to be used with `HttpClientFactory`. Thanks to [@childish-sambino](https://github.com/childish-sambino)!

**Library - Chore**
- [PR #1028](https://github.com/sendgrid/sendgrid-csharp/pull/1028): migrate to new default sendgrid-oai branch. Thanks to [@eshanholtz](https://github.com/eshanholtz)!

**Library - Docs**
- [PR #785](https://github.com/sendgrid/sendgrid-csharp/pull/785): typos fixed. Thanks to [@RodgerLeblanc](https://github.com/RodgerLeblanc)!


[2020-07-08] Version 9.18.0
---------------------------
**Library - Docs**
- [PR #824](https://github.com/sendgrid/sendgrid-csharp/pull/824): Remove references to "Whitelabel". Thanks to [@crweiner](https://github.com/crweiner)!
- [PR #826](https://github.com/sendgrid/sendgrid-csharp/pull/826): cleanup README.md anchors and ToC. Thanks to [@ajloria](https://github.com/ajloria)!

**Library - Feature**
- [PR #1019](https://github.com/sendgrid/sendgrid-csharp/pull/1019): add option for throwing exception on non-successful API request. Thanks to [@childish-sambino](https://github.com/childish-sambino)!

**Library - Chore**
- [PR #1017](https://github.com/sendgrid/sendgrid-csharp/pull/1017): drop Elliptic Curve code in favor of 'starkbank-ecdsa'. Thanks to [@childish-sambino](https://github.com/childish-sambino)!


[2020-06-24] Version 9.17.0
---------------------------
**Library - Fix**
- [PR #916](https://github.com/sendgrid/sendgrid-csharp/pull/916): sendAt timestamp as 32bit integer. Thanks to [@matrix0123456789](https://github.com/matrix0123456789)!

**Library - Feature**
- [PR #1010](https://github.com/sendgrid/sendgrid-csharp/pull/1010): verify signature from event webhook. Thanks to [@childish-sambino](https://github.com/childish-sambino)!
- [PR #1014](https://github.com/sendgrid/sendgrid-csharp/pull/1014): add Elliptic Curve code and utilities. Thanks to [@childish-sambino](https://github.com/childish-sambino)!
- [PR #1011](https://github.com/sendgrid/sendgrid-csharp/pull/1011): Added net40 target framework. Thanks to [@HavenDV](https://github.com/HavenDV)!


[2020-06-10] Version 9.16.0
---------------------------
**Library - Test**
- [PR #1008](https://github.com/sendgrid/sendgrid-csharp/pull/1008): Ensuring default serialization settings behave correctly. Thanks to [@duncanchung](https://github.com/duncanchung)!

**Library - Feature**
- [PR #922](https://github.com/sendgrid/sendgrid-csharp/pull/922): Integrate SendGrid with HttpClientFactory. Thanks to [@akunzai](https://github.com/akunzai)!


[2020-05-20] Version 9.15.1
---------------------------
**Library - Fix**
- [PR #1001](https://github.com/sendgrid/sendgrid-csharp/pull/1001): add back client constructors with client options and actually use its reliability settings. Thanks to [@childish-sambino](https://github.com/childish-sambino)!

**Library - Docs**
- [PR #906](https://github.com/sendgrid/sendgrid-csharp/pull/906): Update Readme to C# 7.x syntax. Thanks to [@aevitas](https://github.com/aevitas)!


[2020-05-13] Version 9.15.0
---------------------------
**Library - Fix**
- [PR #938](https://github.com/sendgrid/sendgrid-csharp/pull/938): Ensures the serialization of the SendGridMessage is untainted by defaults set by applications on the JsonSerializer. Thanks to [@duncanchung](https://github.com/duncanchung)!
- [PR #996](https://github.com/sendgrid/sendgrid-csharp/pull/996): migrate to common prism setup. Thanks to [@childish-sambino](https://github.com/childish-sambino)!

**Library - Feature**
- [PR #997](https://github.com/sendgrid/sendgrid-csharp/pull/997): add support for Twilio Email. Thanks to [@childish-sambino](https://github.com/childish-sambino)!


[2020-04-29] Version 9.14.1
---------------------------
**Library - Fix**
- [PR #993](https://github.com/sendgrid/sendgrid-csharp/pull/993): refactor and fix personalization inserts/updates. Thanks to [@childish-sambino](https://github.com/childish-sambino)!


[2020-04-15] Version 9.14.0
---------------------------
**Library - Feature**
- [PR #991](https://github.com/sendgrid/sendgrid-csharp/pull/991): ignore duplicate email addresses when serializing the message. Thanks to [@childish-sambino](https://github.com/childish-sambino)!
- [PR #905](https://github.com/sendgrid/sendgrid-csharp/pull/905): Implement IEquatable in EmailAddress. Thanks to [@aevitas](https://github.com/aevitas)!

**Library - Fix**
- [PR #924](https://github.com/sendgrid/sendgrid-csharp/pull/924): validate API key is non-empty. Thanks to [@aevitas](https://github.com/aevitas)!
- [PR #903](https://github.com/sendgrid/sendgrid-csharp/pull/903): handle @ in display name when using MailHelper.StringToEmailAddress. Thanks to [@Fieora](https://github.com/Fieora)!

**Library - Docs**
- [PR #990](https://github.com/sendgrid/sendgrid-csharp/pull/990): baseline all the templated markdown docs. Thanks to [@childish-sambino](https://github.com/childish-sambino)!


[2020-04-01] Version 9.13.2
---------------------------
**Library - Docs**
- [PR #989](https://github.com/sendgrid/sendgrid-csharp/pull/989): support verbiage for login issues. Thanks to [@adamchasetaylor](https://github.com/adamchasetaylor)!


[2020-03-18] Version 9.13.1
---------------------------
**Library - Docs**
- [PR #983](https://github.com/sendgrid/sendgrid-csharp/pull/983): Fix CC being added as BCC in csharp sample. Thanks to [@o-farooq](https://github.com/o-farooq)!

**Library - Fix**
- [PR #967](https://github.com/sendgrid/sendgrid-csharp/pull/967): Remove char set from content headers for JSON payloads. Thanks to [@marius-stanescu](https://github.com/marius-stanescu)!


[2020-03-04] Version 9.13.0
---------------------------
**Library - Feature**
- [PR #981](https://github.com/sendgrid/sendgrid-csharp/pull/981): sanity-check email address and address lists when adding to a message. Thanks to [@indy-singh](https://github.com/indy-singh)!


[2020-02-19] Version 9.12.7
---------------------------
**Library - Chore**
- [PR #936](https://github.com/sendgrid/sendgrid-csharp/pull/936): Fix StyleCop warnings. Thanks to [@akunzai](https://github.com/akunzai)!


[2020-01-26] Version 9.12.6
---------------------------
**Library - Fix**
- [PR #975](https://github.com/sendgrid/sendgrid-csharp/pull/975): update setup to allow strong naming on Travis. Thanks to [@jnyrup](https://github.com/jnyrup)!


[2020-01-17] Version 9.12.5
---------------------------
**Library - Fix**
- [PR #971](https://github.com/sendgrid/sendgrid-csharp/pull/971): nuget push flag. Thanks to [@thinkingserious](https://github.com/thinkingserious)!


[2020-01-17] Version 9.12.4
---------------------------
**Library - Chore**
- [PR #970](https://github.com/sendgrid/sendgrid-csharp/pull/970): update travis build command. Thanks to [@thinkingserious](https://github.com/thinkingserious)!


[2020-01-17] Version 9.12.3
---------------------------
**Library - Chore**
- [PR #969](https://github.com/sendgrid/sendgrid-csharp/pull/969): fix travis path for automated deploy. Thanks to [@thinkingserious](https://github.com/thinkingserious)!


[2020-01-17] Version 9.12.2
---------------------------
**Library - Chore**
- [PR #966](https://github.com/sendgrid/sendgrid-csharp/pull/966): deploy command needs a source URL. Thanks to [@thinkingserious](https://github.com/thinkingserious)!


[2020-01-16] Version 9.12.1
---------------------------
**Library - Chore**
- [PR #965](https://github.com/sendgrid/sendgrid-csharp/pull/965): prep the repo for automated releasing. Thanks to [@thinkingserious](https://github.com/thinkingserious)!
- [PR #753](https://github.com/sendgrid/sendgrid-csharp/pull/753): Adds deploy phase that deploys to NuGet. Thanks to [@Gimly](https://github.com/Gimly)!

**Library - Docs**
- [PR #764](https://github.com/sendgrid/sendgrid-csharp/pull/764): Update documentation for retrieving all templates. Thanks to [@tony-ho](https://github.com/tony-ho)!


[2019-08-15] Version 9.12.0
---------------------------
- [PR #892](https://github.com/sendgrid/sendgrid-csharp/pull/892) Replace nuspec and AssemblyInfo with csproj. Big thanks to [Jonas Nyrup](https://github.com/jnyrup) for the PR!
- [PR #876](https://github.com/sendgrid/sendgrid-csharp/pull/876) Add `EmailAddress` attribute. Big thanks to [Jonathan](https://github.com/vanillajonathan) for the PR!
- [PR #839](https://github.com/sendgrid/sendgrid-csharp/pull/839) Refactored SendGridClient to support inject external managed HttpClient. Big thanks to [Charley Wu](https://github.com/akunzai) for the PR!

## [9.11.0] - 2019-04-18
## Added
- [PR #877](https://github.com/sendgrid/sendgrid-csharp/pull/877) Twilio SMS example and branding update. 
- [PR #790](https://github.com/sendgrid/sendgrid-csharp/pull/790) Change the environment variable placeholders to be consistent amongst all example projects. Big thanks to [Ross Macey](https://github.com/RossMacey) for the PR!
- [PR #783](https://github.com/sendgrid/sendgrid-csharp/pull/783) Update documentation with new Git workflow. Big thanks to [Tony Ho](https://github.com/tony-ho) for the PR!
- [PR #782](https://github.com/sendgrid/sendgrid-csharp/pull/782) Update contribution to use Gitflow workflow. Big thanks to [Anatoly](https://github.com/anatolyyyyyy) for the PR!
- [PR #814](https://github.com/sendgrid/sendgrid-csharp/pull/814) Modification in Prerequisites for installation. Big thanks to [Rishabh](https://github.com/Rishabh04-02) for the PR!
- [PR #817](https://github.com/sendgrid/sendgrid-csharp/pull/817) Added Announcement. Big thanks to [Kris Choi](https://github.com/krischoi07) for the PR!

## Fixed
- [PR #741](https://github.com/sendgrid/sendgrid-csharp/pull/741) Fix release date in changelog. Big thanks to [Niladri Dutta](https://github.com/Niladri24dutta) for the PR!
- [PR #751](https://github.com/sendgrid/sendgrid-csharp/pull/751) Update to remove compiler warnings based on stylecop/xunit. Big thanks to [Garry Dixon](https://github.com/dixong) for the PR!
- [PR #754](https://github.com/sendgrid/sendgrid-csharp/pull/754) Removed references to Microsoft.AspNetCore.Http.Abstractions package. Big thanks to [Jeremy Cantu](https://github.com/Jac21) for the PR!
- [PR #794](https://github.com/sendgrid/sendgrid-csharp/pull/794) Updated broken links to examples. Big thanks to [Sanjay Singh](https://github.com/sanjaysingh) for the PR!
- [PR #791](https://github.com/sendgrid/sendgrid-csharp/pull/791) Fixed typo in mail example. Big thanks to [Daredevil Geek](https://github.com/daredevilgeek) for the PR!
- [PR #767](https://github.com/sendgrid/sendgrid-csharp/pull/767) Directly link online version of CLA and fix email mentions. Big thanks to [Bharat Raghunathan](https://github.com/Bharat123rox) for the PR!
- [PR #762](https://github.com/sendgrid/sendgrid-csharp/pull/762) TROUBLESHOOTING.md broken link fix. Big thanks to [Arshad Kazmi](https://github.com/arshadkazmi42) for the PR!

## [9.10.0] - 2018-09-12
## Added
- [PR #724](https://github.com/sendgrid/sendgrid-csharp/pull/724) Add Dynamic Template Support. Big thanks to [Carl Hartshorn](https://github.com/carl-hartshorn) for the PR!
- [PR #643](https://github.com/sendgrid/sendgrid-csharp/pull/643) HTML to Plain text documentation. Big thanks to [Jorge Durán](https://github.com/ganchito55) for the PR!
- [PR #627](https://github.com/sendgrid/sendgrid-csharp/pull/627) Add Code Review to Contributing.md. Big thanks to [Thomas Alrek](https://github.com/thomas-alrek) for the PR!
- [PR #618](https://github.com/sendgrid/sendgrid-csharp/pull/618) Added links to code base in CONTRIBUTION.md. Big thanks to [Kunal Garg](https://github.com/Kunalgarg2100) for the PR!
- [PR #617](https://github.com/sendgrid/sendgrid-csharp/pull/617) Added codecov support. Big thanks to [Rishabh Chaudhary](https://github.com/Rishabh04-02) for the PR!
- [PR #610](https://github.com/sendgrid/sendgrid-csharp/pull/610) Added test to check the license date. Big thanks to [Vitor Mascena Barbosa](https://github.com/VitorBarbosa) for the PR!
- [PR #601](https://github.com/sendgrid/sendgrid-csharp/pull/601) Update .Net 4.5 References and update the targetFramework monikers. Big thanks to [Felipe Leusin](https://github.com/felipeleusin) for the PR!
- [PR #586](https://github.com/sendgrid/sendgrid-csharp/pull/586) Allow for duplicate keys in QueryParams. Big thanks to [Florian Hofmair](https://github.com/ImbaKnugel) for the PR!
- [PR #720](https://github.com/sendgrid/sendgrid-csharp/pull/720) Update CONTRIBUTING.md for improved readability. Big thanks to [Anshul Singhal](https://github.com/af4ro) for the PR!
- [PR #718](https://github.com/sendgrid/sendgrid-csharp/pull/718) Update README badge. Big thanks to [Anshul Singhal](https://github.com/af4ro) for the PR!
- [PR #701](https://github.com/sendgrid/sendgrid-csharp/pull/701) Improved environment variable setup instructions. Big thanks to [Siddharth Kochar](https://github.com/sidkcr) for the PR!
- [PR #537](https://github.com/sendgrid/sendgrid-csharp/pull/537) Added .NET WebForms example. Big thanks to [Roel de Vries](https://github.com/roel-de-vries) for the PR!
- [PR #522](https://github.com/sendgrid/sendgrid-csharp/pull/522) Update add attachment interface. Big thanks to [Graham Mueller](https://github.com/shortstuffsushi) for the PR!
- [PR #609](https://github.com/sendgrid/sendgrid-csharp/pull/609) Added unittest to check for specific repo files. Big thanks to [Manjiri Tapaswi](https://github.com/mptap) for the PR!
- [PR #528](https://github.com/sendgrid/sendgrid-csharp/pull/528) Made DeserializeResponseBody asynchronous. Big thanks to [Xavier Hahn](https://github.com/Gimly) for the PR!
- [PR #531](https://github.com/sendgrid/sendgrid-csharp/pull/531) Skip invalid content objects during serialization. Big thanks to [Roel de Vries](https://github.com/roel-de-vries) for the PR!
- [PR #557](https://github.com/sendgrid/sendgrid-csharp/pull/557) Add Dockerfile. Big thanks to [George Vanburgh](https://github.com/FireEater64) for the PR!
- [PR #599](https://github.com/sendgrid/sendgrid-csharp/pull/599) Add a .env_sample file, update gitignore, update README.md. Big thanks to [thepriefy](https://github.com/thepriefy) for the PR!
- [PR #596](https://github.com/sendgrid/sendgrid-csharp/pull/596) Add github PR template. Big thanks to [Alex](https://github.com/pushkyn) for the PR!
- [PR #582](https://github.com/sendgrid/sendgrid-csharp/pull/582) SEO Friendly Section links. Big thanks to [Alex](https://github.com/pushkyn) for the PR!
- [PR #581](https://github.com/sendgrid/sendgrid-csharp/pull/581) Add/Update Badges on README. Big thanks to [Alex](https://github.com/pushkyn) for the PR!
- [PR #554](https://github.com/sendgrid/sendgrid-csharp/pull/554) Add a Code Of Conduct. Big thanks to [Henrik Bergqvist](https://github.com/hbbq) for the PR!
- [PR #548](https://github.com/sendgrid/sendgrid-csharp/pull/548) Add status codes to USAGE.md. Big thanks to [Hank McCord](https://github.com/InKahootz) for the PR!
- [PR #532](https://github.com/sendgrid/sendgrid-csharp/pull/532) Added information about setting up environment variables. Big thanks to [Roel de Vries](https://github.com/roel-de-vries) for the PR!
- [PR #523](https://github.com/sendgrid/sendgrid-csharp/pull/523) README improvements. Big thanks to [Kaylyn Sigler](https://github.com/ksigler7) for the PR!
- [PR #513](https://github.com/sendgrid/sendgrid-csharp/pull/513) Reduced duplication in Integration tests. Big thanks to [Dylan Morley](https://github.com/dylan-asos) for the PR!
- [PR #514](https://github.com/sendgrid/sendgrid-csharp/pull/514) Added UI thread information. Big thanks to [Matt Bernier](https://github.com/mbernier) for the PR!
- [PR #515](https://github.com/sendgrid/sendgrid-csharp/pull/515) README improvements. Big thanks to [Matt Bernier](https://github.com/mbernier) for the PR!
- [PR #518](https://github.com/sendgrid/sendgrid-csharp/pull/518) README improvements. Big thanks to [Matt Bernier](https://github.com/mbernier) for the PR!

## Fixed
- [PR #735](https://github.com/sendgrid/sendgrid-csharp/pull/735) Fix #418 groupsToDisplay should be optional.
- [PR #719](https://github.com/sendgrid/sendgrid-csharp/pull/719) Update travis config to use a relative path. Big thanks to [Maxim Rubis](https://github.com/siburny) for the PR!
- [PR #702](https://github.com/sendgrid/sendgrid-csharp/pull/702) Fixed virtual bug. Big thanks to [Phil](https://github.com/psboies) for the PR!
- [PR #700](https://github.com/sendgrid/sendgrid-csharp/pull/700) Add default value for SubstitutionTag parameter. Big thanks to [Vedant Koditkar](https://github.com/KoditkarVedant) for the PR!
- [PR #688](https://github.com/sendgrid/sendgrid-csharp/pull/688) Fix mixup of Response.Body and Response.Header summary. Big thanks to [Ryan David Sheasb](https://github.com/ryan27968) for the PR!
- [PR #635](https://github.com/sendgrid/sendgrid-csharp/pull/635) Fixed "Variable assigned but never used" issue. Big thanks to [Unlocked](https://github.com/TheUnlocked) for the PR!
- [PR #615](https://github.com/sendgrid/sendgrid-csharp/pull/615) Update USAGE.md - fix typo. Big thanks to [Anvesh Chaturvedi](https://github.com/anveshc05) for the PR!
- [PR #613](https://github.com/sendgrid/sendgrid-csharp/pull/613) Update CONTRIBUTING.md - fix typo. Big thanks to [thepriefy](https://github.com/thepriefy) for the PR!
- [PR #669](https://github.com/sendgrid/sendgrid-csharp/pull/669) Update Create Template Version Docs as per sendgrid support request. Big thanks to [Tom Needham](https://github.com/06needhamt) for the PR!
- [PR #603](https://github.com/sendgrid/sendgrid-csharp/pull/603) Minor readability fixes in README. Big thanks to [Rion Williams](https://github.com/rionmonster) for the PR!
- [PR #592](https://github.com/sendgrid/sendgrid-csharp/pull/592) Update USAGE.md - fix typos. Big thanks to [Anatoly](https://github.com/anatolyyyyyy) for the PR!
- [PR #608](https://github.com/sendgrid/sendgrid-csharp/pull/608) Updated the LICENSE.txt file to have the correct date range. Big thanks to [Duarte Fernandes](https://github.com/duartefq) for the PR!
- [PR #588](https://github.com/sendgrid/sendgrid-csharp/pull/588) Update USAGE.md - fix typos. Big thanks to [Varun Dey](https://github.com/varundey) for the PR!
- [PR #585](https://github.com/sendgrid/sendgrid-csharp/pull/585) Update USAGE.md - fix typos. Big thanks to [Alex](https://github.com/pushkyn) for the PR!
- [PR #584](https://github.com/sendgrid/sendgrid-csharp/pull/584) Spelling corrections in md, xml docs and variable names. Big thanks to [Brandon Smith](https://github.com/brandon93s) for the PR!
- [PR #527](https://github.com/sendgrid/sendgrid-csharp/pull/527) Corrected list all recipients documentation. Big thanks to [Xavier Hahn](https://github.com/Gimly) for the PR!
- [PR #583](https://github.com/sendgrid/sendgrid-csharp/pull/583) Fix a typo in CONTRIBUTING.md. Big thanks to [ChatPion](https://github.com/ChatPion) for the PR!
- [PR #547](https://github.com/sendgrid/sendgrid-csharp/pull/547) Fix TROUBLESHOOTING.md typo. Big thanks to [Cícero Pablo](https://github.com/ciceropablo) for the PR!
- [PR #546](https://github.com/sendgrid/sendgrid-csharp/pull/546) Fix README.md typo. Big thanks to [Cícero Pablo](https://github.com/ciceropablo) for the PR!
- [PR #529](https://github.com/sendgrid/sendgrid-csharp/pull/529) Update bounce usage Fixes. Big thanks to [Xavier Hahn](https://github.com/Gimly) for the PR!
- [PR #519](https://github.com/sendgrid/sendgrid-csharp/pull/519) Change ContextAwait to ConfigureAwait in TROUBLESHOOTING.md doc. Big thanks to [Brian Surowiec](https://github.com/xt0rted) for the PR!

## [9.9.0] - 2017-08-22
## Added
- #509 Transient Fault Handling (dependency free)
- Implements retry behaviour for transient faults when using HttpClient to send the request.
- Please see #509 and [USE_CASES.md](USE_CASES.md#transient_faults) for details.
- Thanks to [Dylan Morley](https://github.com/dylan-asos) for the PR!

## [9.8.0] - 2017-08-15
## Added
- Single email to multiple recipients - Toggle display of recipients #508
- The method `CreateSingleEmailToMultipleRecipients` now has an additional optional parameter to control whether the email recipients can see each others email addresses. Please see [USE_CASES.md](USE_CASES.md#singleemailmultiplerecipients) for details.
- Thanks to [Niladri Dutta](Niladri24dutta) for the PR!

## [9.7.0] - 2017-08-03
## Added
- Reverted to version 9.5.2, per the conversation here: #501
- Polly (which implemented Transient Fault Handling) was removed in favor of a dependency free solution.
- Transient Fault Handling with Polly still exists in version 9.6.1, if needed

## [9.6.1] - 2017-08-03
## Fixed
- Fix for issue #501: Could not load file or assembly 'Polly, Version=5.2.0.0, Culture=neutral, PublicKeyToken=null'
- The dependency for Polly has been corrected

## [9.6.0] - 2017-08-02
## Added
- Pull #497: Transient Fault Handling
- Thanks to [Dylan Morley](https://github.com/dylan-asos) for the PR!

## [9.5.2] - 2017-7-24
## Fix
- Issue #494: Update pdb format for VSTS
- Thanks to [Murray Crowe](https://github.com/murraybiscuit) for the fix!

## [9.5.1] - 2017-7-20
## Fix
- Issue #494: Repair bad pdb file 

## [9.5.0] - 2017-5-17
## Update
- PR #478: Disable Json.Net reference handling for SendGrid objects 
- Thanks to [sepptruetsch](https://github.com/sepptruetsch) for the PR!

## [9.4.1] - 2017-5-17
## Update
- PR #459: Replace if statements with function
- Thanks to [Jef Statham](https://github.com/JefStat) for the PR!

## [9.4.0] - 2017-5-17
## Update
- PR #458: Add a StringToEmailAddress helper function
- Thanks to [Jef Statham](https://github.com/JefStat) for the PR!

## [9.3.0] - 2017-5-16
## Update
- PR #456: Fixed #403 Implements an interface for mocking and DI
- Thanks to [Nate](https://github.com/nate-fr) for the PR!

## [9.2.1] - 2017-5-16
## Fix
- PR #457: Tos, Bccs and CCs fields could be null
- Thanks to [Jef Statham](https://github.com/JefStat) for the PR!

## [9.2.0] - 2017-05-05
## Update
- PR #444: AddTo, AddBcc, AddCc and SetFrom that just takes an email and name 
- Solves Issue #408
- Thanks to [Paritosh Baghel](https://github.com/paritoshmmmec) for the PR!

## [9.1.1] - 2017-04-12
## Fix
- PR #358: SendGridClient.SendEmailAsync now throws original exception
- Thanks to [Otto Dandenell](https://github.com/ottomatic) for the PR!

## [9.1.0] - 2017-03-20
## Update
- PR #405: Reuse HTTP Client
- Thanks to [Jonny Bekkum](https://github.com/jonnybee) for the PR!

## [9.0.12] - 2017-02-17
## Update
- Allow for empty strings to be passed in as text or html content in *All* of the MailHelpers

## [9.0.11] - 2017-02-17
## Update
- Issue #399: Allow for empty strings to be passed in as text or html content in the MailHelper
- Thanks to [@Angry-Leprechaun](https://github.com/Angry-Leprechaun) for the heads up!

## [9.0.10] - 2017-02-16
## Fix
- Issue #395: Remove dependency for Microsoft.AspNetCore.Http.Abstractions in .NET 4
- Thanks to [@Hinni](https://github.com/Hinni) for the heads up! 

## [9.0.9] - 2017-02-15 ##
## Fix
- Issue #396: Make 'stylecop' a development dependency
- Thanks to [@knopa](https://github.com/knopa) for the heads up!

## [9.0.8] - 2017-02-14 ##
## Update
- Issue #394: Issue with CreateSingleEmail
- You can now have either a null plain/text or plain/html type in the MailHelper
- Thanks to [@onionhammer](https://github.com/onionhammer) for the heads up!

## [9.0.7] - 2017-02-14 ##
### BREAKING CHANGE
- Support for .NET Standard 1.3
- Removed dynamic dependencies
- Updated Mail Helper

## [8.0.4] - 2016-08-24 ##
### Added
- Table of Contents in the README
- Added a [USE_CASES.md](USE_CASES.md) section, with the first use case example for transactional templates

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
- [Troubleshooting](TROUBLESHOOTING.md) section 

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
