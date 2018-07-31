${
    using Typewriter.Extensions.WebApi;
    Template(Settings settings)
    {
        settings.OutputFilenameFactory = file => 
        {
            var FinalFileName = file.Name.Replace("Controller", "");
            FinalFileName = FinalFileName.Replace(".cs", "");
            return $"{FinalFileName}.service.ts";
        };
    }
    // Change ApiController to Service
    string ServiceName(Class c) => c.Name.Replace("ApiController", "Service");
    // Turn IActionResult into void
    string ReturnType(Method objMethod) 
    {
        if(objMethod.Type.Name == "IActionResult")
        {
                if((objMethod.Parameters.Where(x => !x.Type.IsPrimitive).FirstOrDefault() != null))
                {
                    return objMethod.Parameters.Where(x => !x.Type.IsPrimitive).FirstOrDefault().Name;
                }
                else
                {
                    return "void";
                }
        } 
        else
        {
                return objMethod.Type.Name;
        }
    }
    // Get the non primitive paramaters so we can create the Imports at the
    // top of the service
    string ImportsList(Class objClass)
    {
        var ImportsOutput = "";
        // Get the methods in the Class
        var objMethods = objClass.Methods;
        // Loop through the Methdos in the Class
        foreach(Method objMethod in objMethods)
        {
            // Loop through each Parameter in each method
            foreach(Parameter objParameter in objMethod.Parameters)
            {
                // If the Paramater is not prmitive we need to add this to the Imports
                if(!objParameter.Type.IsPrimitive){
                    ImportsOutput = objParameter.Name;
                }
            }
            if(!objMethod.Type.IsPrimitive)
            {
              ImportsOutput = objMethod.Type.name;
            }
        }
        // Notice: As of now this will only return one import
        return  $"import {{ { ImportsOutput } }} from '../Entities/{ImportsOutput}';";
    }
    // Format the method based on the return type
    string MethodFormat(Method objMethod)
    {
        if(objMethod.HttpMethod() == "get"){
            return  $"<{objMethod.Type.Name}>(_Url)";
        } 
        
        if(objMethod.HttpMethod() == "post"){
            return  $"(_Url, {objMethod.Parameters[0].name})";
        }
        if(objMethod.HttpMethod() == "put"){
            return  $"(_Url, {objMethod.Parameters[1].name})";
        }
        if(objMethod.HttpMethod() == "delete"){
            return  $"(_Url)";
        }
        
        return  $"";
    }
}
${
//The do not modify block below is intended for the outputed typescript files... }
//*************************DO NOT MODIFY**************************
//
//THESE FILES ARE AUTOGENERATED WITH TYPEWRITER AND ANY MODIFICATIONS MADE HERE WILL BE LOST
//PLEASE VISIT http://frhagn.github.io/Typewriter/ TO LEARN MORE ABOUT THIS VISUAL STUDIO EXTENSION
//
//*************************DO NOT MODIFY**************************
import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'; 
import { Observable } from 'rxjs';

$Classes(:Controller)[$ImportsList
@Injectable()
export class $ServiceName {
    constructor(private _httpClient: HttpClient) { }        
    $Methods[
    // $HttpMethod: $Url      
    $name($Parameters[$name: $Type][, ]): Observable<$ReturnType> {
        var _Url = `$Url`;
            return this._httpClient.$HttpMethod$MethodFormat;
    }
    ]
}]
