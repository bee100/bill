﻿

//*************************DO NOT MODIFY**************************
//
//THESE FILES ARE AUTOGENERATED WITH TYPEWRITER AND ANY MODIFICATIONS MADE HERE WILL BE LOST
//PLEASE VISIT http://frhagn.github.io/Typewriter/ TO LEARN MORE ABOUT THIS VISUAL STUDIO EXTENSION
//
//*************************DO NOT MODIFY**************************
import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'; 
import { Observable } from 'rxjs';

import { testDto } from '../Entities/testDto';
@Injectable()
export class TestController {
    constructor(private _httpClient: HttpClient) { }        
    
    // get: /api/values      
    get(): Observable<TestDto[]> {
        var _Url = `/api/values`;
            return this._httpClient.get<TestDto[]>(_Url);
    }
    
}