import './App.css'
import { WeatherForecastTable } from './components/WeatherForecastTable'

function App() {
  return (
    <>
      <section id="center">
        <h1>🌤️ Weather Forecast</h1>
        <p>5-day forecast from the Muscle Plus 4000 API</p>
        <WeatherForecastTable />
      </section>
    </>
  )
}

export default App
