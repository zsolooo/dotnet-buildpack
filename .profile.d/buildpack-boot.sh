DNX_BIN=`find $HOME/.dnx/runtimes -type d -name bin`
export PATH="$PATH:$HOME/mono/bin:$DNX_BIN"
export LD_LIBRARY_PATH="$LD_LIBRARY_PATH:$HOME/mono/lib:$HOME/libuv/lib"
export MONO_OPTIONS=--server
