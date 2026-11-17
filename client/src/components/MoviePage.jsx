import { api } from '../api.js'
import { useLoad } from '../useLoad.js'
import { TIME_ZONE_NOTE } from '../format.js'
import SessionList from './SessionList.jsx'

export default function MoviePage({ movieId }) {
  const movie = useLoad(() => api.getMovie(movieId), [movieId])
  const sessions = useLoad(() => api.getSessions({ movieId }), [movieId])

  if (movie.loading && !movie.data) return <p>Loading...</p>
  if (movie.error) return <p className="message error">{movie.error}</p>

  const { title, director, runtime, releaseYear, description } = movie.data

  return (
    <section className="movie-page">
      <a className="back-link" href="#/movies">← All films</a>

      <div className="movie-hero">
        <h2>{title}</h2>
        <p className="movie-meta">
          by {director} · {runtime} min{releaseYear > 0 && ` · ${releaseYear}`}
        </p>
        {description && <p className="movie-description">{description}</p>}
      </div>

      <h3>Upcoming sessions</h3>
      <p className="hint">{TIME_ZONE_NOTE} Sorted from nearest.</p>
      {sessions.error && <p className="message error">{sessions.error}</p>}
      {sessions.loading && !sessions.data
        ? <p>Loading sessions...</p>
        : sessions.data && (sessions.data.length
          ? <SessionList sessions={sessions.data} showMovie={false} />
          : <p className="empty">No upcoming sessions for this film.</p>)}
    </section>
  )
}
