FROM microsoft/dotnet:1.0-sdk

# Install Prism
ADD https://raw.githubusercontent.com/stoplightio/prism/master/install.sh install.sh
RUN chmod +x ./install.sh && \
    ./install.sh && \
    rm ./install.sh

# Clone sources
WORKDIR /root/sources
RUN git clone https://github.com/sendgrid/sendgrid-csharp.git

# Set up symlink
RUN ln -s /root/sources/sendgrid-csharp /root/sendgrid-csharp
WORKDIR /root/sendgrid-csharp

# Restore packages
RUN dotnet restore

# Set up prism
ENV OAI_SPEC_URL="https://raw.githubusercontent.com/sendgrid/sendgrid-oai/master/oai_stoplight.json"
RUN mkdir prism/bin && ./prism/prism.sh
ENV PATH="${PATH}:$PWD/prism/bin/"

# Hack to prevent tests from attempting to start (windows) prism
ENV TRAVIS="true"

# Start prism with the container
COPY entrypoint.sh entrypoint.sh
RUN chmod +x entrypoint.sh

# Set entrypoint
ENTRYPOINT ["./entrypoint.sh"]
CMD ["--mock"]