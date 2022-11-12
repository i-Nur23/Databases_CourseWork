import React, {useEffect, useState } from 'react'

function ChangeNumber(){
    const [pilots, setPilots] = useState([]);
    const [pilot, setPilot] = useState(null);
    const [number, setNumber] = useState(null);

    useEffect(() => {
        (
            async () => {

                const response = await fetch('api/pilot/nums', {
                    headers: {'Content-Type': 'application/json'},
                });

                var content = await response.json();
                setPilots(content.pilots);
                setPilot(content.pilots[0].id)
            }
        )();
    }, []);

    const swapNums = async () =>{
        var pilotToPost = {
            id: pilot,
            number: number
        }

        await fetch('api/pilot/nums', {
            method: 'POST', headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(pilotToPost),
        });
        window.location.reload();
    }

    const changePilot = e => {
        setPilot(e.target.value);
        console.log(pilot)
    }

    const changeNumber = e => {
        setNumber(e.target.value)
    }

    const pilotWithTheNum = num => {
        var pilotWithNum = pilots.find(p => p.carsNumber === num)
        if (typeof(pilotWithNum) != "undefined"){
            return "( "+pilotWithNum.name +" "+ pilotWithNum.surName+ " )"
        }
    }

    return(<div>
        <center>
            <h2>Выберете пилотов, номера которых вы планируете поменять</h2>
            <h5>Если хотите просто убрать номер, оставьте вторую плашку пустой</h5>
        </center>
        <div className='d-flex justify-content-center mx-auto' style={{marginTop:'100px'}}>

            <div className='mx-3'>
                <select value={pilot?.id} onChange={changePilot}  class="form-select" id="numField" aria-describedby="numHelp">
                    {pilots.map(x => 
                        <option 
                            value={x?.id}>
                                {x.name} {x.surName} {x.carsNumber === 0 ? "-" : "№ " + x.carsNumber}
                        </option>    
                    )}
                </select>
            </div>

            <div className='mx-3'>
                <select value={number} onChange={changeNumber}  class="form-select" id="numField" aria-describedby="numHelp">
                    <option value = {null}><p style={{color:'grey'}} className='right-options'>Без номера</p></option> 
                    {Array.from(Array(99).keys()).map(x => 
                        <option value={x+1}>№ {x+1} {pilotWithTheNum(x+1)}</option>    
                    )}
                </select>
            </div>

        </div>
        <div className='d-flex justify-content-center mx-auto my-5'>
            <button className='btn btn-dark' onClick={swapNums}>
                Произвести замену &nbsp;&nbsp;
                <i class="bi bi-arrow-left-right"></i>
            </button> 
        </div>
    </div>)
}

export default ChangeNumber