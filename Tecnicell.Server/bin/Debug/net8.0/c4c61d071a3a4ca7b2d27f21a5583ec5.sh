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

ps 18137;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 18137 > /dev/null;
done;

for child in $(list_child_processes 18140);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/dante/Documents/GitHub/Tecnicell/Tecnicell.Server/bin/Debug/net8.0/c4c61d071a3a4ca7b2d27f21a5583ec5.sh;
