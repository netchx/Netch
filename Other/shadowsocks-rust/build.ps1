Set-Location (Split-Path $MyInvocation.MyCommand.Path -Parent)

git clone https://github.com/shadowsocks/shadowsocks-rust -b 'v1.11.2' src
if ( -Not $? ) {
    exit $lastExitCode
}
Set-Location src

cargo build --features logging,trust-dns,local,utility,local-http,local-tunnel,local-socks4,multi-threaded,stream-cipher --release
if ( -Not $? ) {
    exit $lastExitCode
}

cp -Force '.\target\release\sslocal.exe' '..\..\release\ss-local.exe'
exit 0