import React, {useEffect, useState} from 'react'
import ConfigureResults from './ConfigureResults';
import {Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';
import ResetChamp from "../Elements/ResetChamp";

function AddPilotsInResult () {
    const [stage, setStage] = useState('');
    const [pilots, setPilots] = useState([]);
    const [toFewPilots, setToFewFilots] = useState(false);
    const [isContinue, setContinue] = useState(false);
    const [checkedState, setCheckedState] = useState();
    const [isChampEnded, setIsChampEnded] = useState(false);
    const [selectAllChecked, setAllChecked] = useState(false);
    const [chosenPilots, setChosenPilots] = useState(new Array());
    

    useEffect(() => {
        (

            async () => {

                const response = await fetch('api/results/add/show', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setPilots(content.pilots);
                setStage(content.stage);
                if (content.stage == null){
                    setIsChampEnded(true);
                }
                setCheckedState(new Array(content.pilots.length).fill(false));
            }
        )();
    }, []);

    const [currentCard, setCurrentCard] = useState(null)
    const [cardList, setCardList] = useState(new Array())

    function dragStartHandler(e,card){
        setCurrentCard(card)
    }

    function dragEndHandler(e){
        e.target.style.background = 'white'
    }

    function dragOverHandler(e){
        e.preventDefault()
        e.target.style.background = 'lightgray'
    }

    function dropHandler(e,card){
        e.preventDefault()
        setChosenPilots(cardList.map(p => {
            if (p.id === card.id){
                return {...p, order: currentCard.order}
            }
            if (p.id === currentCard.id){
                return {...p, order: card.order}
            }

            return p
        }))
        e.target.style.background = 'white'
    }

    const sortCards = (a,b) => {
        if (a.order > b.order){
            return 1
        } else {
            return -1
        }
    }


    const GetChosenPilots = async (array) => {
        const pilots = array.map(x => "pilots="+x).toString().replaceAll(',','&');

        const response = await fetch('api/results/configure/show'+pilots, {
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
        });

        const content = await response.json();
        var pilotsList = content.pilots 
        setChosenPilots(pilotsList);
        var arr =new Array();
        for (let i = 0; i < pilotsList.length; i++){
            var obj = {
                id : pilotsList[i].id,
                order : i + 1,
                text : pilotsList[i].name + " " + pilotsList[i].surName 
            };
            arr.push(obj)
        }
        setCardList(arr)
        
    }


    const handleClick = () => {
        setAllChecked(!selectAllChecked)
        const updatedCheckedState = checkedState.map((item) =>
            item = !selectAllChecked
        );

        setCheckedState(updatedCheckedState);
    }

    const handleClickList = (position) => {
        const updatedCheckedState = checkedState.map((item, index) =>
            index === position ? !item : item
        );

        setCheckedState(updatedCheckedState);
        setToFewFilots(false);
    }

    const handleContinue = () => {
        setContinue(false);
        var checkboxes = document.getElementsByClassName("form-check-input");
        var count = 0;
        for (let i = 0; i < checkboxes.length; i++)
        {
            var cb = checkboxes[i];
            if (cb.checked){
                count ++;
            }
        }
        if (count < 10){
            setToFewFilots(true);
        } else {
            setToFewFilots(false);
            setContinue(true);
        }
    }

    const GetPilots = () =>
            <div className='d-flex justify-content-center'> 
            <ul>
                {pilots.map((pilot, index)=>(
                <li style={{listStyleType:'none'}} key={index}>
                    <div class="form-check ">
                        <input class="form-check-input pilot-choice"  
                               type="checkbox" 
                               id={pilot.id} 
                               value=""
                               checked={checkedState[index]} 
                               onChange={() => handleClickList(index)}
                        />
                        <label class="form-check-label" for="flexCheckDefault">
                            {pilot.name}    {pilot.surName} ( № {pilot.carsNumber} ) 
                        </label>
                    </div>
                </li>  
                ))}
                <li style={{listStyleType:'none'}}>
                    <div class="form-check w-100">
                        <input class="form-check-input" type="checkbox" value="" checked={selectAllChecked} id="flexCheckDefault" 
                            onChange={() => handleClick()}
                        />
                        <label class="form-check-label" for="flexCheckDefault" id="selectAll">
                            Выбрать все
                        </label>
                    </div>
                </li>
            </ul>
            </div>
    if (!isChampEnded){
        return(
            <div>
                <h3><center>Следующая гонка:</center></h3>
                <h4><center>Этап №{stage.stageNumber}: {stage.name}</center></h4>
                <br/>
                <h3><center>Выберете участников:</center></h3>

                <form>
                    <GetPilots/>
                </form>

                <center>
                <button className='btn btn-info w-50' onClick={handleContinue}>
                    Продолжить
                </button>
                {toFewPilots ? <p style={{color:'red'}}>Выберете хотя бы 10 пилотов</p> : null}
                </center>
                {isContinue ? <ConfigureResults checkedState={checkedState}/> : null}
            </div>
    )}

    return (
        <ResetChamp/>
    )
}

export default AddPilotsInResult