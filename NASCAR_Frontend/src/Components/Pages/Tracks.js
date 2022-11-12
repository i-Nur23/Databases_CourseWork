import React, {useEffect, useState} from 'react'

function Tracks(){
    
    const [tracks, setTracks] = useState([]);

    useEffect(() => {
        (
            async () => {
                const response = await fetch('api/tracks', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setTracks(content.tracks);
            }
        )();
    }, []);

    const GetTracks = () => 
        <ul>
            {tracks.map((track)=>(
              <li style={{listStyleType:'none'}}>
                <div className='p-2 my-3 shadow border border-2 rounded'>
                    <h2><center>{track.name}</center></h2>
                    <p>Тип трассы: {track.type == "SS" ? "Суперспидвей" : track.type == "ST" ? "Шорт-трек" : "Дорожник"}</p>
                    <p>Длина круга: {track.length} миль(-я)</p>
                    <p>Месторасположение: {track.state}, {track.city}</p>
                </div>
              </li>  
            ))}
        </ul>

    return(
        <div>
            <h1><center><strong>Треки</strong></center></h1>
            <GetTracks/>
        </div>


    )
}

export default Tracks;