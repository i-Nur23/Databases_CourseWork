import react, {useState, useEffect} from 'react'
import {Link, withRouter} from 'react-router-dom'

const RaceInfo = withRouter((props) => {

    const [results, setResults] = useState([]);

    useEffect(() => {
        (
            async () => {
                const response = await fetch('api/results/withNums/'+props.match.params.id, {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setResults(content.results);
            }
        )();
    }, []);


    return(
    <div>
        <center><h2>Итоговый протокол {results[0]?.stage.name}</h2></center>
        <table className='table'>
            <thead>
                <tr>
                    <th className='text-center'>Место</th>
                    <th colSpan={2} className='text-center'>Пилот</th>
                    <th className='text-center'>Машина</th>
                    <th className='text-center'>Команда</th>
                    <th className='text-center'>Отставание от лидера</th>
                    <th className='text-center'>Число пит-стопов</th>
                </tr>
            </thead>
            <tbody>
                {results.map((res, index) => (
                    <tr key={index}>
                        <td align='center'>{res.place}</td>
                        <td colSpan={2} align='center'>{res.pilot.name} {res.pilot.surname}</td>
                        <td align='center'>{res.number}</td>
                        <td align='center'>{res.leaderGap}</td>
                        <td align='center'>{res.numberOfPitStops}</td>
                    </tr>))}
            </tbody>
        </table>
    </div>
    )
})

export default RaceInfo