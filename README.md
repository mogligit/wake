# wake
Simple Wake-On-LAN command line based tool.

Coded in C# using Visual Studio 2019.

## Setup

Download from the [releases page](https://github.com/mogligit/wake/releases).

No install is needed. Extract the ZIP file and **run the file wake.exe through the command line**.

## Usage

`wake <mac> <address>`

Note: the address could be the broadcast address of your network, the packet will still be received. This is useful when the recipient cannot respond to an ARP request by the router, if you attempt to send the packet directly to the last IP assigned to the recipient.
