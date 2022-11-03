import React, {useEffect, useState} from 'react'

function ConfigureResults(props){
    const [chosenPilots, setChosenPilots] = useState(new Array())
    const [currentCard, setCurrentCard] = useState(null)
    const [cardList, setCardList] = useState(new Array())

    useEffect(() => {
        (

            async () => {

                var inputs = document.getElementsByClassName('pilot-choice');
                var array = []
                for (let i = 0; i < props.checkedState.length; i++){
                    if (props.checkedState[i]){
                        array.push(inputs[i].getAttribute('id'))
                    }
                }

                const pilots = array.map(x => "pilots="+x).toString().replaceAll(',','&');

                const response = await fetch('api/addresult/configure?'+pilots, {
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
                        pilot : pilotsList[i].name + " " + pilotsList[i].surName,
                        number : "машина: " + pilotsList[i].carsNumber
                };
                    arr.push(obj)
                }
                    setCardList(arr)
                }
        )();
    }, []);


    const dragStartHandler = (e,card) => {
        setCurrentCard(card)
    }

    const dragEndHandler = (e) => {
        e.target.style.background = 'white'
    }

    const dragOverHandler = (e) => {
        e.preventDefault()
        e.target.style.background = 'lightgray'
    }

    const dropHandler = (e,card) => {
        e.preventDefault()
        setCardList(cardList.map(p => {
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

    const postRace = async () => {
        await fetch('api/addresult/configure', {
            method: 'POST', headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            body: JSON.stringify(cardList.map(x => {x.id, x.order})),
        });
    }


        return(
            <div className='my-4 p-2 shadow border border-2 rounded'>
                <br/>
                <h3 className='p-2'>Порядок финиша:</h3>
                <br/>
                {cardList.sort(sortCards).map(card =>
                    <div
                        onDragStart={(e) => dragStartHandler(e,card)}
                        onDragLeave={(e) => dragEndHandler(e)}
                        onDragEnd={(e) => dragEndHandler(e)}
                        onDragOver={(e) => dragOverHandler(e)}
                        onDrop={(e) => dropHandler(e,card)}
                        draggable={true}
                        className="border rounded-2 m-2 p-2 d-flex justify-content-between"
                    >
                        <div>
                            {card.order}
                        </div>
                        <div><strong>
                            {card.pilot}
                        </strong></div>
                        <div><i>
                            {card.number}
                        </i></div>  
                    </div>
                )
                }
                <button className='btn btn-light' onClick={postRace}>
                    Отправить результаты
                </button>
            </div>
        )
}

export default ConfigureResults