import { useState } from 'react'
import { api } from '../api.js'
import { useLoad } from '../useLoad.js'
import MovieCard from './MovieCard.jsx'
import AddMovieForm from './AddMovieForm.jsx'

export default function MoviesPage() {
  const movies = useLoad(api.getMovies, [])
  const [showForm, setShowForm] = useState(false)
  const [notice, setNotice] = useState(null)

  const handleAdd = async (data) => {
    await api.addMovie(data)
    setShowForm(false)
    setNotice({ type: 'success', text: 'Film added.' })
    movies.reload()
  }

  const handleRemove = async (id) => {
    setNotice(null)
    try {
      await api.deleteMovie(id)
      setNotice({ type: 'success', text: 'Film removed.' })
      movies.reload()
    } catch (err) {
      setNotice({ type: 'error', text: err.message })
    }
  }

  return (
    <section>
      <div className="page-toolbar">
        <h2>Films</h2>
        <button onClick={() => setShowForm(true)}>+ Add Film</button>
      </div>

      {showForm && <AddMovieForm onSubmit={handleAdd} onCancel={() => setShowForm(false)} />}
      {notice && <p className={`message ${notice.type}`}>{notice.text}</p>}
      {movies.error && <p className="message error">{movies.error}</p>}

      {movies.loading && !movies.data ? (
        <p>Loading...</p>
      ) : movies.data?.length === 0 ? (
        <p className="empty">No films yet. Click "Add Film" to add the first one.</p>
      ) : (
        <div className="movies-grid">
          {movies.data?.map((m) => (
            <MovieCard key={m.id} movie={m} onRemove={() => handleRemove(m.id)} />
          ))}
        </div>
      )}
    </section>
  )
}
