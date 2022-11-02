import React, { Component, useEffect, useState } from 'react';
import Cookies from 'js-cookie';
import {Alert} from 'reactstrap';


function AuthAdmin(props){
    const [password, setPassword] = useState('');
    const [error, setError] = useState(false);

    const GiveResponse = async () => {
        var res = await fetch('api/auth', {
            method: 'POST', headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(password),
        });
        var feedback = await res.json();
        if (feedback.message == 'success'){
            props.setToken(Cookies.get("jwt"));
        }
        setError(true);
    }

    const handleChange = event => {
        setPassword(event.target.value);
        setError(false);
    };

    
    return(<div>
        <div className='m-auto my-5 w-25 p-2 mt-5 shadow border border-2 rounded'>
        <div classname="form-group mb-5">
            <label for="KeyPassword" class="form-label mt-4">Введите ключ для входа</label>
                <input type="password" value={password} className = "form-control" id="KeyPassword" placeholder="Ключ" onChange={handleChange}/>
            <button type="submit" class="btn btn-info mt-3" onClick={GiveResponse}>Войти</button>
            {error ? <p style={{color:'red'}}>Неверный ключ</p> : null}
        </div>   
        </div> 
    </div>)
    


}

export default AuthAdmin