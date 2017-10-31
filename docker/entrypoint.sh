#! /bin/bash

# check for + link mounted libraries:
if [ -d /mnt/sendgrid-csharp ]
then
    rm /root/sendgrid-csharp
    ln -s /mnt/sendgrid-csharp /root/sendgrid-csharp
    echo "Linked mounted sendgrid-csharp" 
fi

echo "Welcome to sendgrid-csharp docker."
echo

if [ "$1" != "--no-mock" ]
then
    echo "Starting Prism in mock mode. Calls made to Prism will not actually send emails."
    echo "Disable this by running this container with --no-mock."
    prism run --mock --spec $OAI_SPEC_URL 2> /dev/null &
else
    echo "Starting Prism in live (--no-mock) mode. Calls made to Prism will send emails."
    prism run --spec $OAI_SPEC_URL 2> /dev/null  &
fi
echo "To use Prism, make API calls to localhost:4010. For example,"
echo "    var sg = new SendGridClient(Environment.GetEnvironmentVariable('SENDGRID_API_KEY_CAMPAIGNS'),
                                'http://localhost:4010')"
echo "To stop Prism, run \"kill $!\" from the shell."

cd /root/sendgrid-csharp
exec $SHELL