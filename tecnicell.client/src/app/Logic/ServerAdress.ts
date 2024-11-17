import { environment } from "../../environments/environment";

function server  ()
{ 
    if(!!localStorage.getItem('server')==true)
        return localStorage.getItem('server') 
    else 
        return environment.url
}

export default server;