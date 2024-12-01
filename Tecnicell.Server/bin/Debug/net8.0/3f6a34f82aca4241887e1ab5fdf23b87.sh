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

ps 2012;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 2012 > /dev/null;
done;

for child in $(list_child_processes 2015);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/dante/Documents/GitHub/Tecnicell/Tecnicell.Server/bin/Debug/net8.0/3f6a34f82aca4241887e1ab5fdf23b87.sh;
