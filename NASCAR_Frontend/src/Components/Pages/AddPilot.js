import Resct, {useState, useEffect} from 'react'

function AddPilot(){

    const [name, setName] = useState('');
    const [surname, setSurName] = useState('');
    const [date, setDate] = useState(new Date());
    const [country, setCountry] = useState('');
    const [state, setState] = useState('');
    const [city, setCity] = useState('');
    const [number, setNumber] = useState(0);
    const [status, setStatus] = useState('PT');

    const addPilot = async () => {
        var bday = new Date(date);
        var birtDate = bday.getDate() +"/"+("0" + (bday.getMonth() + 1)).slice(-2)+"/"+bday.getFullYear()
        var pilot = {
            name : name,
            surname : surname,
            country : country,
            state : state,
            city : city,
            number : number,
            birthDate : date,
            status : status
        }
        console.log(typeof(pilot.birthDate));
        console.log(pilot.birthDate);

        var res = await fetch('api/pilot', {
            method: 'POST', headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(pilot),
        });
        var feedback = await res.json();
    }

    const formChange = event => {
        var inputs = document.getElementsByClassName('required');
        Array.prototype.slice.call(inputs)
            .forEach((input) => {
                {
                    input.classList.remove("is-invalid")
                }
            })
    }

    const checkSubmitForm = async event => {
        var inputs = document.getElementsByClassName('required');
        Array.prototype.slice.call(inputs)
            .forEach((input) => {
                if (input.value === ''){
                    input.classList.add("is-invalid")
                }
            });

        await addPilot();
    }

    const isLatinic = (key) => {
        return (/^[A-Z]+$/i.test(key) || key === '')
    }

    const isCyrillic = (key) => {
        return (/^[А-Я]+$/i.test(key) || key === '')
    }

    const is16 = (date) => {
        var now = new Date (Date.now());


        return (date.getFullYear() < now.getFullYear() || date.getMonth() < now.getMonth() || date.getDay() < now.getDay()) &&
                (now.getFullYear() - date.getFullYear() > 16)
    }

    const changeName = (e) =>{
        var key = e.target.value.slice(-1);
        if (isLatinic(key) || e.target.value.length == 1){
            setName(e.target.value);
        }
    }

    const changeSurName = (e) =>{
        var key = e.target.value.slice(-1);
        if (isLatinic(key)){
            setSurName(e.target.value);
        }
    }

    const changeDate = (e) =>{
        var birthdate = new Date(e.target.value);

        var dateInString = birthdate.getFullYear()+"-"+("0" + (birthdate.getMonth() + 1)).slice(-2)+"-"+("0" + (birthdate.getDate())).slice(-2); 
        
        if (is16(birthdate)){
            setDate(dateInString);
        }
        
    }

    const changeCountry = (e) =>{
        var key = e.target.value.slice(-1);
        if (isCyrillic(key)){
            setCountry(e.target.value);
        }
    }

    const changeState = (e) =>{
        var key = e.target.value.slice(-1);
        if (isCyrillic(key)){
            setState(e.target.value);
        }
    }

    const changeCity = (e) =>{
        var key = e.target.value.slice(-1);
        if (isCyrillic(key)){
            setCity(e.target.value);
        }
    }

    const changeNumber = (e) => {
        setNumber(e.target.value);
    }

    const changeStatus = (e) => {
        setStatus(e.target.value);
    }



    const SelectNum = () => {
        var nums = Array.from(Array(100).keys())
        return (
            <select value={number} class="form-select" id="numField" aria-describedby="numHelp" onChange={changeNumber}>
                {nums.map(x => 
                    <option value={x}>{x}</option>    
                )}
            </select>
        )
    }


    return (
        <div>
            <center><h2>Введите данные о новом пилоте</h2></center>
            <div onChange={formChange}>
                <div className='row g-2'>
                    <div className='col-5 p-3'>
                        <label for="nameField" className="form-label">Имя (на латинице) </label>
                        <input type="text" value={name} class="form-control required" id="nameField" aria-describedby="nameHelp" onChange={changeName}/>
                    </div>

                    <div className='col-5 p-3'>
                        <label for="surnameField" className="form-label">Фамилия (на латинице) </label>
                        <input type="text" value={surname} class="form-control required" id="surnameField" aria-describedby="nameHelp" onChange={changeSurName}/>
                    </div>

                    <div className='col-2 p-3'>
                        <label for="dateField" className="form-label">Дата рождения </label>
                        <input type="date" value={date} class="form-control required" id="dateField" aria-describedby="nameHelp" onChange={changeDate}/>
                    </div>
                </div>
                <div className='row g-2'>
                    <div className='col-md p-3'>
                        <label for="countryField" className="form-label">Страна</label>
                        <input type="text" value={country} class="form-control required" id="countryField" aria-describedby="nameHelp" onChange={changeCountry}/>
                    </div>

                    <div className='col-md p-3'>
                        <label for="stateField" className="form-label">Штат (если имеется)</label>
                        <input type="text" value={state} class="form-control" id="stateField" aria-describedby="nameHelp" onChange={changeState}/>
                    </div>

                    <div className='col-md p-3'>
                        <label for="cityField" className="form-label">Город </label>
                        <input type="text" value={city} class="form-control required" id="cityField" aria-describedby="nameHelp" onChange={changeCity}/>
                    </div>

                    <div className='col-sm p-3'>
                        <label for="cityField" className="form-label">Номер</label>
                        <SelectNum/>
                        <div id="numHelp" class="form-text">Оставить 0, если номер еще не определен</div>
                    </div>
                    <div className='col-sm p-3'>
                        <label for="cityField" className="form-label">Статус выступлений</label>
                        <select value={status} class="form-select" onChange={changeStatus}>
                            <option value="PT">Партаймер</option>
                            <option value="ON">Полное расписние</option>
                        </select>
                    </div>
                </div>
                <div className='row g-2 p-3'>
                    <button className='btn btn-primary g-2 px-3 col-3' type="submit" onClick={checkSubmitForm}>
                        Добавить
                    </button>
                </div>
            </div>
        </div>
    )
}

export default AddPilot