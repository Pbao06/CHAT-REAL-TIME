import apiClient from "@/lib/apiClient";
import {LoginDto,RegisterDto} from "@/types/Login"
export const authService= {
    login: (data:LoginDto)=> apiClient.post("/Auth/Login",data),

    register: (data: RegisterDto)=> apiClient.post("/Auth/Register",data)

};