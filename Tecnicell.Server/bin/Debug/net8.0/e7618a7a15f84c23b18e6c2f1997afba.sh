function list_child_processes () {
    local ppid=$1;
    local current_children=$(pgrep -P $ppid);
    local local_child;
    if [ $? -eq 0 ];
    then
        for current_child in $current_children
        do
          local_child=$current_child;
          list_child_processes $local_child;
          echo $local_child;
        done;
    else
      return 0;
    fi;
}

ps 7502;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 7502 > /dev/null;
done;

for child in $(list_child_processes 13217);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/dante/Documents/GitHub/Tecnicell/Tecnicell.Server/bin/Debug/net8.0/e7618a7a15f84c23b18e6c2f1997afba.sh;
