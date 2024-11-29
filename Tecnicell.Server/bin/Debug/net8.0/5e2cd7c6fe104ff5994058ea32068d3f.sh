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

for child in $(list_child_processes 9421);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/dante/Documents/GitHub/Tecnicell/Tecnicell.Server/bin/Debug/net8.0/5e2cd7c6fe104ff5994058ea32068d3f.sh;
