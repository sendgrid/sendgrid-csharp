<a name="transactional-templates"></a>
# Transactional Templates

For this example, we assume you have created a [transactional template](https://sendgrid.com/docs/User_Guide/Transactional_Templates/Create_and_edit_dynamic_transactional_templates.html).
Following is the template content we used for testing.

Template ID (replace with your own):

```text
d-d42b0eea09964d1ab957c18986c01828
```

Email Subject:

```text
Dynamic Subject: {{subject}}
```

Template Body:

```html
<html>
<head>
    <title></title>
</head>
<body>
Hello {{name}},
<br /><br/>
I'm glad you are trying out the dynamic template feature!
<br /><br/>
I hope you are having a great day in {{location.city}} :)
<br /><br/>
</body>
</html>
```
